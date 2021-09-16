using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

[GenerateAuthoringComponent]
public struct MovingPlatform : IComponentData
{
    public float movementSpeed;
    public bool moveRight;    
    public float3 extend;
    public bool isPlayerStand;
}
