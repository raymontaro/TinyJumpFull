using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny;

[GenerateAuthoringComponent]
public struct CheckOnGround : IComponentData
{
    public bool IsGrounded;
    public bool dirRight;
    public float3 extend;
}