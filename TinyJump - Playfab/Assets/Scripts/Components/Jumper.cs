using Unity.Entities;

namespace TinyPhysics
{
    [GenerateAuthoringComponent]
    public struct Jumper : IComponentData
    {
        public float jumpImpulse;

        public Entity arrow;        

        public bool JumpTrigger { get; set; }
    }
}
