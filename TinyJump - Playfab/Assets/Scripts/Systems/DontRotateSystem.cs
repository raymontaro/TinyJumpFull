using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class DontRotateSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref DontRotate dontRotate, ref Rotation rotation) =>
        {
            rotation.Value = quaternion.identity;
        });
    }
}
