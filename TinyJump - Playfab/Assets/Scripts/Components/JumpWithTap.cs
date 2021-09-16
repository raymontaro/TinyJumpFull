using Unity.Entities;

namespace TinyPhysics
{
    [GenerateAuthoringComponent]
    public struct JumpWithTap : IComponentData
    {
        public Entity entity;
        public Entity arrow;
    }
}
