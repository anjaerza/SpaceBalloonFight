using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{    
    public void Continue()
    {
        Time.timeScale = 1;
        UIController.Instance.AñadirPuntaje(LvlBonus.Instance.ScoreBonus);     
        LvlBonus.Instance.ScoreBonus=0;   
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void ContinuePause()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("CMProvitionalMenu");
    }

}
