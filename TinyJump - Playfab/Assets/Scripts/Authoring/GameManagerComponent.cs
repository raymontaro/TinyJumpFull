using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;

public class GameManagerComponent : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public string playerName;
    public int currentLevel;
    public GameObject[] levels;
    public bool isOpenLvSelect;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new GameManager { playerName = playerName, currentLv = currentLevel, maxLv = currentLevel, isOpenLvSelect = isOpenLvSelect });

        var buffer = dstManager.AddBuffer<Level>(entity);
        foreach (var l in levels)
        {
            buffer.Add(new Level { level = conversionSystem.GetPrimaryEntity(l) });
        }

        var lvBuffer = dstManager.AddBuffer<TimeRecord>(entity);
        for (int i = 1; i <= 5; i++)
        {
            lvBuffer.Add(new TimeRecord { level = i, time = 0f });
        }
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        foreach (var l in levels)
        {
            referencedPrefabs.Add(l);
        }


    }
}