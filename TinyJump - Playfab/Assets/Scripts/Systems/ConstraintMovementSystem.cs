using Unity.Entities;
using Unity.Transforms;

public class ConstraintMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref ConstraintMovement constraintMovement, ref Translation translation) =>
        {
            if (constraintMovement.x)
            {
                translation.Value.x  = 0;
            }

            if (constraintMovement.y)
            {
                translation.Value.y = 0;
            }

            if (constraintMovement.z)
            {
                translation.Value.z = 0;
            }
        });
    }
}
