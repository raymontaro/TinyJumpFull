var OnLoginSuccessAction;
var GetBestTimeCallbackAction;

var LoginCallback = function (result, error) {
    if (result !== null) {
        //console.log("[javascript]Login Success");
        //console.log(OnLoginSuccessAction);
        OnLoginSuccessCallback();        
    } else if (error !== null) {
        console.log(
            "Something went wrong with your first API call.\n" +
            "Here's some debug information:\n" +
            PlayFab.GenerateErrorReport(error)
            );
    }
}

var OnLoginSuccessCallback = function (){
    dynCall_v(OnLoginSuccessAction);        
}

var GetUserDataCallback = function (result, error) {
    
    if (result !== null && result.data.Data.hasOwnProperty(user)) {
        //console.log(result.data.Data[user].Value);        
        GetBestTimeCallback(result.data.Data[user].Value);
    } else if (error !== null) {
        console.log(
            "Something went wrong with your first API call.\n" +
            "Here's some debug information:\n" +
            PlayFab.GenerateErrorReport(error)
            );
    }
}

var GetBestTimeCallback = function (val){
    var fVal = parseFloat(val)
    
    dynCall_vf(GetBestTimeCallbackAction, fVal);
}

var SetBestTimePlayfabCallback = function (result, error){
    if (result !== null) {
        console.log("Set best time success");              
    } else if (error !== null) {
        console.log(
            "Something went wrong with your first API call.\n" +
            "Here's some debug information:\n" +
            PlayFab.GenerateErrorReport(error)
            );
    }
}