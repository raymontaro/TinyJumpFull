using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Tiny;
using Unity.Physics;
using Unity.Physics.Extensions;

public class MultiplyStraightSystem : ComponentSystem
{
    
    float spawnTimer;
    bool runOnce = false;


    protected override void OnUpdate()
    {
        if (runOnce)
            return;

        spawnTimer -= Time.DeltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = 0.5f;

            Entities.WithAll<MultiplyStraight>().ForEach((ref MultiplyStraight multiplyStraight, ref Translation translation) =>
            {                
                float3 origin = 0f;

                for (int i = 0; i < multiplyStraight.count; i++)
                {
                    Entity spawnedEntity = EntityManager.Instantiate(multiplyStraight.prefab);

                    var currentPos = translation.Value;


                    currentPos += origin;

                    EntityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(currentPos.x, currentPos.y, 0) });

                    origin += multiplyStraight.direction;
                    //Debug.Log(origin);
                }
            });
        }

        runOnce = true;

    }
}
