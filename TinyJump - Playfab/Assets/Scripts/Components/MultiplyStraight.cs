using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MultiplyStraight : IComponentData
{
    
    public Entity prefab;

    public int count;

    public float3 direction;
}
