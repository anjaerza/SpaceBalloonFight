using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class sceneManagerMenu : MonoBehaviour
{
    AudioSource audio;
    LevelManager manager;
    [SerializeField] GameObject panelLevelSelector;
    [SerializeField] GameObject btnClosePanelLevelSelector;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlayGame()
    {
        /*if (GameManager.Instance.LastLevel == 1)
        {
            SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);

        }*/

       /* if (GameManager.Instance.LastLevel > 1)
        {*/
            panelLevelSelector.SetActive(true);
            btnClosePanelLevelSelector.SetActive(true);
       /* }*/

        audio.Play();
    }

    public void ClosePanelSelectorLevel()
    {
        panelLevelSelector.SetActive(false);
        btnClosePanelLevelSelector.SetActive(false);
    }
    public void GoToMenu()
    {
        audio.Play();
        SceneManager.LoadScene("CMProvitionalMenu");
    }

    public void PlayGameFirstTime()
    {
        SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);
    }
}
