using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Tiny;
using Unity.Mathematics;
using Unity.Physics.Systems;

public class MovingPlatformSystem : ComponentSystem
{
    //bool runOnce = false;
    Random random;

    protected override void OnCreate()
    {
        random = new Random(56);
    }

    protected override void OnUpdate()
    {        
        Entities.ForEach((ref MovingPlatform movingPlatform, ref Translation translation, ref Rotation rotation, ref PhysicsCollider body) =>
        {                  
            ref PhysicsWorld physicsWorld = ref World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>().PhysicsWorld;

            float3 rayOrigin = new float3(translation.Value.x, translation.Value.y + 2f, translation.Value.z);            
            float3 rayDirection = new float3(0, 1, 0);
            float rayDistance = 0.1f;

            int bitmaskAll = ~0;

            CollisionFilter boxfilter = new CollisionFilter { BelongsTo = (uint)bitmaskAll, CollidesWith = 1 << 0 };

            bool boxcast = physicsWorld.BoxCast(rayOrigin, rotation.Value, movingPlatform.extend, rayDirection, rayDistance, out ColliderCastHit boxhit, boxfilter, QueryInteraction.Default);
            if (boxcast)
            {
                movingPlatform.isPlayerStand = true;
                body.Value.Value.Filter = CollisionFilter.Default;
                return;
            }
            else
            {
                body.Value.Value.Filter = CollisionFilter.Zero;
                movingPlatform.isPlayerStand = false;
            }


            if (movingPlatform.moveRight && translation.Value.x > 3f)
                movingPlatform.moveRight = false;

            if (!movingPlatform.moveRight && translation.Value.x < -3f)
                movingPlatform.moveRight = true;

            if (movingPlatform.moveRight)
                translation.Value.x += movingPlatform.movementSpeed * Time.DeltaTime;
            else
                translation.Value.x -= movingPlatform.movementSpeed * Time.DeltaTime;

            //if (runOnce)
            //    return;

            //movingPlatform.movementSpeed = random.NextFloat(1f, 5f);
            //if (random.NextInt(1, 2) == 1)
            //    movingPlatform.moveRight = true;
            //else
            //    movingPlatform.moveRight = false;
            //runOnce = true;
        });
    }
}
