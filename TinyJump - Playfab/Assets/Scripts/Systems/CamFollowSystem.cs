using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Tiny;

public class CamFollowSystem : SystemBase
{    
    protected override void OnUpdate()
    {
        

        Entities.ForEach((ref CamFollow camFollow, ref Translation translation) =>
        {


            float3 followPos = EntityManager.GetComponentData<Translation>(camFollow.entityToFollow).Value;

            float3 pos;

            #region follow
            if (camFollow.followX)
                pos.x = followPos.x + camFollow.offset.x;
            else
                pos.x = translation.Value.x;

            if (camFollow.followY)
                pos.y = followPos.y + camFollow.offset.y;
            else
                pos.y = translation.Value.y;

            if (camFollow.followZ)
                pos.z = followPos.z + camFollow.offset.z;
            else
                pos.z = translation.Value.z;
            #endregion
            
            float3 finalPos = pos;

            #region clamp
            if (camFollow.clampLeft && finalPos.x < camFollow.leftClampValue)
                finalPos.x = camFollow.leftClampValue;

            if (camFollow.clampRight && finalPos.x > camFollow.rightClampValue)
                finalPos.x = camFollow.rightClampValue;

            if (camFollow.clampTop && finalPos.x > camFollow.topClampValue)
                finalPos.y = camFollow.topClampValue;

            if (camFollow.clampBottom && finalPos.y < camFollow.bottomClampValue)
                finalPos.y = camFollow.bottomClampValue;
            #endregion

            translation.Value = finalPos;

            

        }).WithoutBurst().Run();
    }

    
    
}
