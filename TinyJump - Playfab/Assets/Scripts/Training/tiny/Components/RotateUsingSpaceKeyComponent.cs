using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct RotateUsingSpaceKeyComponent : IComponentData
{
    public float3 rotateValue;
    public float3 currentRot;
}
