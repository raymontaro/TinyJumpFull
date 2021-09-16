mergeInto(LibraryManager.library, {

    PlayfabLogin: function (titleId, customId) {
      var str = UTF8ToString(titleId); // create a JavaScript string from a null-terminated UTF-8 string allocated on the heap, pointed by pStr
      var str2 = UTF8ToString(customId); // create a JavaScript string from a null-terminated UTF-8 string allocated on the heap, pointed by pStr
      console.log("" + str + " " + str2);

      PlayFab.settings.titleId = str;
      
      var loginRequest = {
        // Currently, you need to look up the required and optional keys for this object in the API reference for LoginWithCustomID. See the Request Headers and Request Body.
        TitleId: PlayFab.settings.titleId,
        CustomId: str2,
        CreateAccount: true
      };

      PlayFabClientSDK.LoginWithCustomID(loginRequest, LoginCallback);

    },

    SetOnLoginSuccess: function (pOnLoginSuccess){
        //console.log("[JavaScript] SetSimpleCallback");
        
        OnLoginSuccessAction = pOnLoginSuccess;
        
        /*setTimeout(function(){
            dynCall_v(pOnLoginSuccess);
        }, 0);*/
    },

    GetBestTime: function(customId){  
        var str = UTF8ToString(customId);
        
        var getUserDataRequest = {
            Keys: null
        };

        PlayFabClientSDK.GetUserData(getUserDataRequest, GetUserDataCallback);        
    },

    SetGetBestTimeCallback: function (p){
        
        
        GetBestTimeCallbackAction = p;
        
        
    },

    SetBestTimePlayfab:function(val){
        var str = UTF8ToString(val);
        var dataPayload = {};

        dataPayload[user] = str;
        
        var UpdateRequest={
            Data: dataPayload
        };

        PlayFabClientSDK.UpdateUserData(UpdateRequest, SetBestTimePlayfabCallback);
    },
          
  });  