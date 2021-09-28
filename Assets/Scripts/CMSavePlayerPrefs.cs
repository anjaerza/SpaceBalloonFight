using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CMSavePlayerPrefs :  Singleton<CMSavePlayerPrefs>
{
    [SerializeField] Text scoreText;
    [SerializeField] TMP_InputField playerName;

    private float tempScore;

    private void Start() {

        tempScore=UIController.Instance.Score;
    }

    private void Update()
    {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         PlayerPrefs.SetFloat("Score", tempScore);
    //         PlayerPrefs.Save();
    //         print("Saved!");
    //     }

    //     if (Input.GetKeyDown(KeyCode.L)) 
    //     {
    //         scoreText.text = PlayerPrefs.GetFloat("Score").ToString();
    //         print("Loaded!");
        // }
    }


    public void SavePlayerPrefs(){
        PlayerPrefs.SetString("nickname",playerName.text);
         PlayerPrefs.SetFloat("score", tempScore);
            PlayerPrefs.Save();
            // print("Saved!");
    }

    public void LoadPlayerPrefs(){
        playerName.text=PlayerPrefs.GetString("nickname");
        scoreText.text = PlayerPrefs.GetFloat("score").ToString();
        // print("Loaded!");
    }
}
