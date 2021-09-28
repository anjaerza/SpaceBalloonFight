using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardUI : Singleton<LeaderBoardUI>
{
    [SerializeField] int limitOfLeaderboard;

    [SerializeField] GameObject playerScorePrefab;
    [SerializeField] GameObject container;

    [SerializeField] GameObject myPlayerScorePrefab;
    [SerializeField] GameObject myPlayerScoreContainer;

    List<LeaderboardData> myLeaderboards = new List<LeaderboardData>();

    public List<LeaderboardData> MyLeaderboards { get => myLeaderboards; set => myLeaderboards = value; }

    protected override void Awake()
    {

    }

    private void Start()
    {
        SetLeaderboard();
    }

    public void SetLeaderboard()
    {
        //OLD
        /*
        foreach (LeaderboardData leaderboardData in myLeaderboards)
        {
            GameObject prefabInstance = Instantiate(playerScorePrefab, container.transform);
            prefabInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = leaderboardData.PlayerUsername;
            prefabInstance.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = leaderboardData.PlayerScore.ToString();
        }
        */

        //NEW 
        for (int i = 0; i < myLeaderboards.Count; i++)
        {
            if (i < limitOfLeaderboard)
            {
                GameObject prefabInstance = Instantiate(playerScorePrefab, container.transform);
                prefabInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = myLeaderboards[i].PlayerUsername;
                prefabInstance.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = myLeaderboards[i].PlayerScore.ToString();
                prefabInstance.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            }

            if (myLeaderboards[i].PlayerUsername == PlayerPrefs.GetString("NICKNAME"))
            {
                GameObject myPlayerPrefabInstance = Instantiate(myPlayerScorePrefab, myPlayerScoreContainer.transform);
                myPlayerPrefabInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = myLeaderboards[i].PlayerUsername;
                myPlayerPrefabInstance.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = myLeaderboards[i].PlayerScore.ToString();
                myPlayerPrefabInstance.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            }
        }
    }

    public void closeLeaderboar()
    {
        gameObject.SetActive(false);
    }
}