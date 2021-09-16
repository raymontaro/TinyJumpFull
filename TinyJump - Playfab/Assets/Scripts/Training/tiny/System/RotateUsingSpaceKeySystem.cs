using Unity.Entities;
using Unity.Transforms;
using Unity.Tiny.Input;
using Unity.Mathematics;

public class RotateUsingSpaceKeySystem : ComponentSystem
{
    
    protected override void OnUpdate()
    {
        var input = World.GetExistingSystem<InputSystem>();

        Entities.ForEach((ref RotateUsingSpaceKeyComponent rotateComponent,ref Rotation rotation) =>
        {
            if (input.GetKey(KeyCode.Space))
            {
                rotateComponent.currentRot += (rotateComponent.rotateValue * Time.DeltaTime);
                
                
                quaternion rot = quaternion.EulerXYZ(rotateComponent.currentRot);
                rotation.Value = rot;                
            }
        });
    }
       
}
