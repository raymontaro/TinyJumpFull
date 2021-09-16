using Unity.Entities;

[GenerateAuthoringComponent]
public struct DestroyByPlayer : IComponentData
{
    public Entity entity;
    public bool destroyNow;    
}
