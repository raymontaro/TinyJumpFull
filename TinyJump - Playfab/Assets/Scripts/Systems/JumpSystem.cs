using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Tiny;
using Unity.Transforms;

namespace TinyPhysics.Systems
{
    /// <summary>
    ///     Detect when a jump trigger is set in order to apply a vertical impulse
    /// </summary>
    public class JumpSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref PhysicsVelocity velocity, ref PhysicsMass mass, ref Jumper jumper, ref SpriteRenderer spriteRenderer, ref CheckOnGround checkOnGround) =>
            {
                //var settingsEntity = GetSingletonEntity<PlayerSprite>();
                //var settings = EntityManager.GetBuffer<PlayerSprite>(settingsEntity);

                if (jumper.JumpTrigger)
                {
                    

                    var rot = EntityManager.GetComponentData<Rotation>(jumper.arrow);

                    //Debug.Log("RotZ = " + rot.Value.value.z);

                    if (rot.Value.value.z > 0)
                    {
                        //if (checkOnGround.IsGrounded)
                        //    spriteRenderer.Sprite = settings[0].entity;
                        //else
                        //    spriteRenderer.Sprite = settings[1].entity;
                        checkOnGround.dirRight = true;
                    }
                    else
                    {
                        //if (checkOnGround.IsGrounded)
                        //    spriteRenderer.Sprite = settings[2].entity;
                        //else
                        //    spriteRenderer.Sprite = settings[3].entity;
                        checkOnGround.dirRight = false;
                    }

                    // Jump by applying an impulse on y Axis
                    velocity.ApplyLinearImpulse(mass, new float3(rot.Value.value.z*jumper.jumpImpulse, jumper.jumpImpulse, 0));

                    AudioUtils.PlaySound(EntityManager, AudioTypes.Jump);

                    // Consume trigger
                    jumper.JumpTrigger = false;
                }
            }).WithStructuralChanges().Run();
        }
    }
}
