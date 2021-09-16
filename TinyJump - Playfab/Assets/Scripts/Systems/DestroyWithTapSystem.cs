using Unity.Entities;
using Unity.Tiny;

public class DestroyWithTapSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<DestroyWithTap>().ForEach((ref Tappable tappable, ref DestroyWithTap destroyWithTap) =>
        {
            EntityManager.AddComponent(destroyWithTap.entity, ComponentType.ReadWrite<Disabled>());

            if (tappable.IsTapped)
            {
                tappable.IsTapped = false;

                Debug.Log("tapped");

                EntityManager.DestroyEntity(destroyWithTap.entity);
                //EntityManager.AddComponent(destroyWithTap.entity, ComponentType.ReadWrite<Disabled>());
                //EntityManager.SetEnabled(destroyWithTap.entity, false);
            }
        });
    }
}
