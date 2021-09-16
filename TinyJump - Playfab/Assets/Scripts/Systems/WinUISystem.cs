using Unity.Entities;
using Unity.Tiny;
using Unity.Tiny.UI;
using Unity.Scenes;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public class WinUISystem : SystemBase
{
    public bool isWin = false;
    public int currentLv = 1;    

    protected override void OnUpdate()
    {
        

        var uiSys = World.GetExistingSystem<ProcessUIEvents>();
        var startEntity = uiSys.GetEntityByUIName("StartMenu");
        if (startEntity == Entity.Null)
            return;

        var startButton = uiSys.GetEntityByUIName("startButton");
        var startButtonState = GetComponent<UIState>(startButton);

        var winEntity = uiSys.GetEntityByUIName("WinUI");

        if (winEntity == Entity.Null)
            return;

        var restartButton = uiSys.GetEntityByUIName("restartButton");
        var restartButtonState = GetComponent<UIState>(restartButton);

        
                       

        //if (restartButtonState.IsClicked)
        //{
        //    var buttonTransform = GetComponent<RectTransform>(winEntity);
        //    buttonTransform.Hidden = true;
        //    SetComponent(winEntity, buttonTransform);
        //}


        var winTransform = GetComponent<RectTransform>(winEntity);
        winTransform.Hidden = !isWin;
        SetComponent(winEntity, winTransform);



        if (restartButtonState.IsClicked)
        {
            AudioUtils.PlaySound(EntityManager, AudioTypes.Button);
            var sceneSystem = World.GetExistingSystem<SceneSystem>();
            var level1SceneEntity = GetSingletonEntity<Lv1>();
            var level1Scene = EntityManager.GetComponentData<SceneReference>(level1SceneEntity);
            var p = GetSingletonEntity<CheckOnGround>();
            EntityManager.SetComponentData<Translation>(p, new Translation { Value = new float3(0f, -6.9f, 0f) });
            EntityManager.SetComponentData<PhysicsVelocity>(p, new PhysicsVelocity { Linear = float3.zero });
            sceneSystem.UnloadScene(level1SceneEntity);
            isWin = false;
            sceneSystem.LoadSceneAsync(level1Scene.SceneGUID, new SceneSystem.LoadParameters { AutoLoad = true, Flags = SceneLoadFlags.LoadAdditive });

            BestTimeSystem.ResetTimer();
        }

        if (startEntity == Entity.Null)
            return;

        if (startButtonState.IsClicked)
        {
            AudioUtils.PlaySound(EntityManager, AudioTypes.Button);
            AudioUtils.PlaySound(EntityManager, AudioTypes.Jump);
            AudioUtils.PlaySound(EntityManager, AudioTypes.Star);

            var startMenuTransform = GetComponent<RectTransform>(startEntity);
            startMenuTransform.Hidden = true;
            SetComponent(startEntity, startMenuTransform);


            var sceneSystem = World.GetExistingSystem<SceneSystem>();
            var level1SceneEntity = GetSingletonEntity<Lv1>();
            var level1Scene = EntityManager.GetComponentData<SceneReference>(level1SceneEntity);
            var p = GetSingletonEntity<CheckOnGround>();
            EntityManager.SetComponentData<Translation>(p, new Translation { Value = new float3(0f, -6.9f, 0f) });
            EntityManager.SetComponentData<PhysicsVelocity>(p, new PhysicsVelocity { Linear = float3.zero });
            sceneSystem.UnloadScene(level1SceneEntity);
            isWin = false;
            sceneSystem.LoadSceneAsync(level1Scene.SceneGUID, new SceneSystem.LoadParameters { AutoLoad = true, Flags = SceneLoadFlags.LoadAdditive });

            //var gameManagerEntity = GetSingletonEntity<GameManager>();
            //var openLvSelect = EntityManager.GetComponentData<GameManager>(gameManagerEntity);
            //openLvSelect.isOpenLvSelect = true;
            //SetComponent(gameManagerEntity, openLvSelect);

            BestTimeSystem.PlayTimer();
        }
    }

    
}
