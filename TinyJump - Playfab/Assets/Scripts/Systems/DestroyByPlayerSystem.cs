using Unity.Entities;
using Unity.Tiny;

public class DestroyByPlayerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<DestroyByPlayer>().ForEach((ref DestroyByPlayer destroyByPlayer) =>
        {
            //Debug.Log(destroyByPlayer.entity.Index);
            if (destroyByPlayer.destroyNow)
            {
                destroyByPlayer.destroyNow = false;

                AudioUtils.PlaySound(EntityManager, AudioTypes.Star);

                //Debug.Log(destroyByPlayer.entity.Index);
                //Debug.Log("destroy");
                //Debug.Log(destroyByPlayer.entity.Index);

                //EntityManager.DestroyEntity(destroyByPlayer.entity);
                EntityManager.SetEnabled(destroyByPlayer.entity, false);
                World.GetExistingSystem<WinUISystem>().isWin = true;                
            }
            
        });
    }
}

