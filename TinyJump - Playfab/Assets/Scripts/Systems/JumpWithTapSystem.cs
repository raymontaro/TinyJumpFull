using Unity.Entities;
using Unity.Tiny;
using Unity.Transforms;
using Unity.Mathematics;

namespace TinyPhysics.Systems
{
    /// <summary>
    ///     Detect when an object is tapped and set its jump trigger
    /// </summary>
    //[UpdateBefore(typeof(MovementSystem))]
    public class JumpWithTapSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<JumpWithTap>().ForEach((ref Tappable tappable, ref JumpWithTap jumpWithTap) =>
            {                
                

                if (tappable.IsTapped)
                {
                    tappable.IsTapped = false;

                    var jumpEn = jumpWithTap.entity;

                    if (EntityManager.GetComponentData<CheckOnGround>(jumpEn).IsGrounded)
                    {
                        // Set jump trigger
                        EntityManager.SetComponentData<Jumper>(jumpEn, new Jumper { JumpTrigger = true, jumpImpulse = 10f, arrow = jumpWithTap.arrow });
                        //jumper.JumpTrigger = true;
                    }

                    // Consume tap
                    tappable.IsTapped = false;
                }                
            }).WithoutBurst().Run();
        }
    }
}
