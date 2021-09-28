using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class LvlBonus : Singleton<LvlBonus>
{
    private int scoreBonus;
    private int bubbleCounter;

    private int bubbleCountDisapear;

    public int ScoreBonus
    {
        get { return scoreBonus; }
        set { scoreBonus = value; }
    }
    public int BubbleCounter
    {
        get { return bubbleCounter; }
        set { bubbleCounter = value; }
    }

    public int BubbleCountDisapear { get => bubbleCountDisapear; set => bubbleCountDisapear = value; }
    public GameObject[] KillCollider1 { get => KillCollider; set => KillCollider = value; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI ballonsText;
    [SerializeField] private TextMeshProUGUI scoreballonText;


    [SerializeField] GameObject [] KillCollider;
    
    protected override void Awake(){

    }
    

    public void ActivePanel()
    {
        panel.SetActive(true);       

        if(ballonsText != null && scoreballonText != null)
        {
            ballonsText.text = bubbleCounter.ToString();

        if(LevelManager.Instance.CurrentLevel==4){

             scoreballonText.text = bubbleCounter.ToString() + " x 500";          

             scoreBonus = bubbleCounter * 500;

        }
        if(LevelManager.Instance.CurrentLevel==9){
             scoreballonText.text = bubbleCounter.ToString() + " x 700";          

             scoreBonus = bubbleCounter * 700;
        }

           

        }

        Time.timeScale = 0;
    }


    public void SumarBurbujaDesaparecida()
    {
        bubbleCountDisapear++;
        if (bubbleCountDisapear == 8)
        {
            KillCollider[0].SetActive(false);
            KillCollider[1].SetActive(false);
            KillCollider[2].SetActive(false);
            KillCollider[3].SetActive(false);
            ActivePanel();
        }
    }

}



