using PlayFab;
using PlayFab.ClientModels;
//using PlayFab.PfEditor.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPlayFabController : Singleton<CMPlayFabController>
{
    string nickname;

    [SerializeField] GameObject nicknamePanel;
    [SerializeField] GameObject panelFalla;

    protected override void Awake()
    {
        nickname = "Guest";
        base.Awake();
    }

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "B5400";
        }

        if (PlayerPrefs.HasKey("NICKNAME"))
        {
            nickname = PlayerPrefs.GetString("NICKNAME");

            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
        }
        else
        {
#if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
#endif
#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginIOSSuccess, OnLoginIOSFailure);
#endif
        }
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    private void OnLoginAndroidSuccess(LoginResult result)
    {
        Debug.Log("Android Loged!");

        if (!PlayerPrefs.HasKey("NICKNAME"))
        {
            nicknamePanel.SetActive(true);
        }
    }

    private void OnLoginAndroidFailure(PlayFabError error)
    {
        panelFalla.SetActive(true);
        Debug.LogError(error.GenerateErrorReport());
    }

    public void OnClickAddDisplayName()
    {
        var updateDisplayNameRequest = new UpdateUserTitleDisplayNameRequest { DisplayName = nickname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(updateDisplayNameRequest, OnAddDisplayNameSuccess, OnLoginAndroidFailure);
    }

    private void OnAddDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name");

        PlayerPrefs.SetString("NICKNAME", result.DisplayName);
        PlayerPrefs.Save();

        nicknamePanel.SetActive(false);
    }

    public void GetNickname(string nicknameIn)
    {
        nickname = nicknameIn;
    }

    // Build the request object and access the API
    public void StartCloudUpdatePlayerScore()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerScore", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { highScore = UIController.Instance.Score }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdatePlayerScore, OnErrorShared);
    }
    // OnCloudHelloWorld defined in the next code block

    private static void OnCloudUpdatePlayerScore(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        //JsonObject jsonResult = (JsonObject)result.FunctionResult;
        //object messageValue;
        //jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        //Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void CloseErrorPanel()
    {
        panelFalla.SetActive(false);
    }
}