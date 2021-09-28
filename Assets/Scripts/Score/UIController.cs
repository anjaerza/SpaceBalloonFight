using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;


public class UIController : Singleton<UIController>
{
    private int score = 0;

    int lastScore;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private string scoreText = "Score: ";

    public TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI txtLevel;
    protected override void Awake(){
        
    }
    void Update()
    {
        if (textScore != null)
        {
            textScore.text = scoreText + score.ToString();            
        }
        if(txtLevel!=null)
        {
            txtLevel.text = "Level:"+" "+LevelManager.Instance.CurrentLevel.ToString();
        }
        
    }

    public void AñadirPuntaje(int scoreañadido)
    {
        score += scoreañadido;

        lastScore = scoreañadido;
    }

    public void ShowScore(GameObject enemy)
    {
        GameObject canvasScore = enemy.transform.GetChild(0).gameObject;
        Text scoreText = canvasScore.GetComponentInChildren<Text>();
        scoreText.text = lastScore.ToString();
        canvasScore.SetActive(true);
        StartCoroutine(ReleaseShowScore(canvasScore));
    }

    private IEnumerator ReleaseShowScore(GameObject canvasScore)
    {
        yield return new WaitForSeconds(0.5f);
        //canvasScore.SetActive(false);
    }
}
    