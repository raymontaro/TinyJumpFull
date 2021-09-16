using Unity.Entities;
//using PlayFab;
//using PlayFab.ClientModels;
using System.Runtime.InteropServices;
using Unity.Tiny;
using System;

[UpdateBefore(typeof(TryJavascriptSystem))]
public class PlayfabSystem : ComponentSystem
{
    internal class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute() { }
        public MonoPInvokeCallbackAttribute(Type t) { }
    }

    [DllImport("__Internal")]
    private static extern void PlayfabLogin(string titleId, string customId);

    [DllImport("__Internal")]
    private static extern void GetBestTime();

    [DllImport("__Internal")]
    private static extern void SetBestTime(string titleId, string customId);

    [DllImport("__Internal")]
    private static extern void SetOnLoginSuccess(Action callback);

    [DllImport("__Internal")]
    private static extern void SetGetBestTimeCallback(GetBestTimeCallbackDelegate callback);

    [DllImport("__Internal")]
    private static extern void SetBestTimePlayfab(string val);


    public static string username;
    public static float bestTime = 0f;

    protected override void OnCreate()
    {
        //var request = new LoginWithCustomIDRequest { CustomId = "9D469F7910DA8E35" };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);


        PlayfabLogin("F5BA4", "9D469F7910DA8E35");
        
        
        //Debug.Log(TryJavascriptSystem.username);
        username = TryJavascriptSystem.username;

        SetOnLoginSuccess(OnLoginSuccess);
        SetGetBestTimeCallback(GetBestTimeCallback);
    }

    protected override void OnUpdate()
    {
        
    }


    [MonoPInvokeCallback(typeof (Action))]
    private static void OnLoginSuccess()
    {
        Debug.Log("[c#]Login successfull");
        GetBestTime();
    }

    //void OnLoginFailure(PlayFabError error)
    //{
    //    Debug.LogError("Login failed");
    //}

    public static void SetBestTime(float time)
    {
        bestTime = time;

        string valT = bestTime.ToString();
        SetBestTimePlayfab(valT);

        //PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        //{
        //    Data = new Dictionary<string, string>()
        //        {
        //            { username, time.ToString("0")}
        //        }
        //},
        //result => { Debug.Log("best time is set"); },
        //error => { Debug.LogError(error.GenerateErrorReport()); }
        //);
    }

    /*public static void GetBestTime()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {

            Keys = null
        },
        result =>
        {
            float gettingTime;
            if (result.Data != null && result.Data.ContainsKey(username))
            {
                float.TryParse(result.Data[username].Value, out gettingTime);
                bestTime = gettingTime;
            }
        },
        error => { Debug.LogError(error.GenerateErrorReport()); }
        );
    }*/

    delegate void GetBestTimeCallbackDelegate(float val);

    [MonoPInvokeCallback(typeof(GetBestTimeCallbackDelegate))]
    private static void GetBestTimeCallback(float val)
    {
        
        bestTime = val;

        Debug.Log("BestTime = " + bestTime);
    }

}
