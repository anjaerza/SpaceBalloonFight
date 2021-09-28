using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPlayFabLogin : Singleton<CMPlayFabLogin>
{
    string userEmail;
    string userPass;
    string username;

    public GameObject loginPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;

    public GameObject leaderboardPanel;

    protected override void Awake()
    {

    }

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "B5400";
        }

        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPass = PlayerPrefs.GetString("PASSWORD");

            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
#if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);
#endif
#if UNITY_IOS

#endif
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Loged!");

        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPass);

        loginPanel.SetActive(false);
        recoverButton.SetActive(false);
    }

    private void OnLoginAndroidSuccess(LoginResult result)
    {
        Debug.Log("Android Loged!");

        loginPanel.SetActive(false);
        //recoverButton.SetActive(true);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered!");

        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPass);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnLoginAndroidFailure);

        loginPanel.SetActive(false);
    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + "is your new display name");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPass, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnLoginAndroidFailure(PlayFabError error)
    {
        Debug.Log("Error");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        //Debug.LogError(error.GenerateErrorReport());
        Debug.Log("Error");
        Debug.Log(error.GenerateErrorReport());

    }

    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passIn)
    {
        userPass = passIn;
    }

    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    public void OpenAddLogin()
    {
        addLoginPanel.SetActive(true);
    }

    public void OnClickAddLogin()
    {
        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPass, Username = username };
        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoginSuccess, OnRegisterFailure);
    }

    private void OnAddLoginSuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Registered!");

        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPass);

        addLoginPanel.SetActive(false);
    }

    #region Leaderboard

    public void GetLeaderboarder()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerHighScore", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeadboard, OnErrorLeaderboard);
    }

    void OnGetLeadboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);

        //Debug.Log(result.Leaderboard[0].StatValue);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            //Debug.Log(player.DisplayName + ": " + player.StatValue);

            LeaderboardData leaderboardData = new LeaderboardData(player.DisplayName, player.StatValue);
            LeaderBoardUI.Instance.MyLeaderboards.Add(leaderboardData);
        }
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion Leaderboard

    #region SubmitScore

    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "PlayerHighScore",
                Value = playerScore
            }
        }
        }, result => OnStatisticsUpdated(result), FailureCallback);
    }

    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult)
    {
        Debug.Log("Successfully submitted high score");
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion
}
