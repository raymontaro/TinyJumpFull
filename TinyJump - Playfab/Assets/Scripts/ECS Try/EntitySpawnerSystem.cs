﻿using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class EntitySpawnerSystem : ComponentSystem
{
    float spawnTimer;
    Random random;

    protected override void OnCreate()
    {
        random = new Random(56);
    }

    protected override void OnUpdate()
    {
        spawnTimer -= Time.DeltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = 0.5f;

            Entities.ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
            {
                Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabEntity);

                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(random.NextFloat(-5f, 5f), random.NextFloat(-5f, 5f), 0) });
            });
        }
    }
    
}
