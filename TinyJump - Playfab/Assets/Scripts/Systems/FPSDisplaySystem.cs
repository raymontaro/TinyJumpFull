using Unity.Entities;
using Unity.Tiny.UI;
using Unity.Tiny.Text;
using Unity.Tiny;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public class FPSDisplaySystem : SystemBase
{
    //float deltaTime = 0.0f;

    protected override void OnUpdate()
    {
        //deltaTime += (Time.DeltaTime - deltaTime) * 0.1f;

        //float msec = deltaTime * 1000.0f;
        //float fps = 1.0f / deltaTime;

        //string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        int current = 0;
        current = (int)(1f / Time.DeltaTime);

        string text = "FPS: " + current + " fps";

        var uiSys = World.GetExistingSystem<ProcessUIEvents>();
        var textEntity = uiSys.GetEntityByUIName("FPSDisplay");
        if(textEntity!=Entity.Null)
            TextLayout.SetEntityTextRendererString(EntityManager, textEntity, text);

        //Debug.Log("fps = " + textEntity.Index);
    }
}
