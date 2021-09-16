using Unity.Entities;
using Unity.Tiny;
using Unity.Tiny.UI;
using Unity.Scenes;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public class LevelSelectSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        //Debug.Log("asd");

        var uiSys = World.GetExistingSystem<ProcessUIEvents>();
        var lvSelectEntity = uiSys.GetEntityByUIName("LevelSelectMenu");

        //Debug.Log("dsa");

        var gameManagerEntity = GetSingletonEntity<GameManager>();
        //Debug.Log("qwe");
        var gameManager = EntityManager.GetComponentData<GameManager>(gameManagerEntity);

        //Debug.Log(lvSelectEntity.Index);
        //Debug.Log(gameManager.isOpenLvSelect);

        var lvSelectTransform = GetComponent<RectTransform>(lvSelectEntity);
        lvSelectTransform.Hidden = !gameManager.isOpenLvSelect;
        SetComponent(lvSelectEntity, lvSelectTransform);
    }    
}
