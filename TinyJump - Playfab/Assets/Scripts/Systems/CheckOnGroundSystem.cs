using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Tiny;
using Unity.Tiny.Rendering;
using Unity.Transforms;

public class CheckOnGroundSystem : ComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();

        RequireSingletonForUpdate<PlayerSprite>();             
    }

    protected override void OnUpdate()
    {        

        Entities.ForEach((ref CheckOnGround checkOnGround, ref Translation translation, ref Rotation rotation, ref SpriteRenderer spriteRenderer, ref PhysicsVelocity velocity) =>
        {
            var settingsEntity = GetSingletonEntity<PlayerSprite>();
            var settings = EntityManager.GetBuffer<PlayerSprite>(settingsEntity);

            ref PhysicsWorld physicsWorld = ref World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>().PhysicsWorld;

            float3 rayOrigin1 = new float3(translation.Value.x+0.35f, translation.Value.y - 1.25f, translation.Value.z);
            float3 rayOrigin2 = new float3(translation.Value.x-0.35f, translation.Value.y - 1.25f, translation.Value.z);
            float3 rayDirection = new float3(0, -1, 0);
            float rayDistance = 0.5f;

            var rayInput1 = new RaycastInput
            {
                Start = rayOrigin1,
                End = rayOrigin1 + rayDirection * rayDistance,
                Filter = CollisionFilter.Default
            };

            var rayInput2 = new RaycastInput
            {
                Start = rayOrigin2,
                End = rayOrigin2 + rayDirection * rayDistance,
                Filter = CollisionFilter.Default
            };

            bool raycast1 = physicsWorld.CastRay(rayInput1, out Unity.Physics.RaycastHit hit1);
            bool raycast2 = physicsWorld.CastRay(rayInput2, out Unity.Physics.RaycastHit hit2);

            
            int boxFilterCollideWith = ~((1 << 0) | (1<<1));

            CollisionFilter boxFilter = new CollisionFilter { BelongsTo = 1 << 2, CollidesWith =  (uint)boxFilterCollideWith};            

            bool boxcast = physicsWorld.BoxCast(new float3(translation.Value.x, translation.Value.y - 1.2f, translation.Value.z),rotation.Value,checkOnGround.extend,rayDirection,rayDistance,out ColliderCastHit boxhit,boxFilter,QueryInteraction.Default);
            //Debug.Log("boxcast = " + boxcast+ " hit = " + boxhit.Position);

            //Debug.Log("1 " + raycast1 + " 2 " + raycast2);

            #region starDestroyer
            CollisionFilter starFilter = new CollisionFilter { BelongsTo = 1 << 1, CollidesWith = 1 << 1 };

            bool starTrigger = physicsWorld.BoxCast(translation.Value, rotation.Value, new float3(0.7f, 0.85f, 0.7f), new float3(0f, 0.1f, 0f), 0.01f, out ColliderCastHit starhit, starFilter, QueryInteraction.Default);

            if (starTrigger)
            {
                //tityManager.DestroyEntity(starhit.Entity);
                
                EntityManager.SetComponentData<DestroyByPlayer>(starhit.Entity, new DestroyByPlayer { destroyNow = true, entity = starhit.Entity });
                //velocity.Linear = float3.zero;
                //velocity.Angular = float3.zero;

                BestTimeSystem.StopTimer();

                if (PlayfabSystem.bestTime == 0f)
                {
                    PlayfabSystem.SetBestTime(BestTimeSystem.currentTimer);
                }
                else
                {
                    if (PlayfabSystem.bestTime > BestTimeSystem.currentTimer)
                    {
                        PlayfabSystem.SetBestTime(BestTimeSystem.currentTimer);
                    }
                }
            }
            #endregion



            if (boxcast)
            {
                checkOnGround.IsGrounded = true;
                if(checkOnGround.dirRight)
                    spriteRenderer.Sprite = settings[0].entity;
                else
                    spriteRenderer.Sprite = settings[2].entity;
            }
            else
            {
                checkOnGround.IsGrounded = false;
                if(checkOnGround.dirRight)
                    spriteRenderer.Sprite = settings[1].entity;
                else
                    spriteRenderer.Sprite = settings[3].entity;
            }            

        });
    }
}
