using Unity.Entities;

[GenerateAuthoringComponent]
public struct CurrentTime : IComponentData
{
    public Entity entity;
}
