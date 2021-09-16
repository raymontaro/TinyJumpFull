using Unity.Entities;

[GenerateAuthoringComponent]
public struct ConstraintMovement : IComponentData
{
    public bool x;
    public bool y;
    public bool z;
}
