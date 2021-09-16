using System;
using Unity.Entities;
using System.Runtime.InteropServices;
using Unity.Tiny.UI;
using Unity.Tiny.Text;
using Unity.Tiny;

public class TryJavascriptSystem : ComponentSystem
{
    [DllImport("__Internal")]
    private static extern void PrintHello();

    [DllImport("__Internal")]
    private static extern void PrintString(string str);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string ReceiveString();

    [DllImport("__Internal")]
    private static extern string ReceiveUser();

    public static string username;

    protected override void OnCreate()
    {


        //PrintHello();
        //PrintString("A string passed from c# to javascript");
        //Console.WriteLine("[C#] AddNumbers: " + AddNumbers(5, 7));
        //Console.WriteLine("[C#] ReceiveString: " + ReceiveString());

        username = ReceiveUser();
        Console.WriteLine("userName: " + username);
    }

    protected override void OnUpdate()
    {
        

        Entities.WithAll<Username>().ForEach((ref Username un) =>
        {
            string text = "username: " + username;

            //Debug.Log(un.entity.Index);

            TextLayout.SetEntityTextRendererString(EntityManager, un.entity, text);
        });

        //string text = "username: " + ReceiveUser();

        //var uiSys = World.GetExistingSystem<ProcessUIEvents>();
        //var textEn = uiSys.GetEntityByUIName("usernameText");
        //if (textEn != Entity.Null)
        //    TextLayout.SetEntityTextRendererString(EntityManager, textEn, text);

        

    }

    
}
