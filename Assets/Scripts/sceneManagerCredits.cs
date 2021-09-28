using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class sceneManagerCredits : MonoBehaviour
{
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void GoToCredits()
    {
        audio.Play();
        SceneManager.LoadScene("Creditos");
    }

    public void GoToMenu()
    {	
	audio.Play();
        SceneManager.LoadScene("CMProvitionalMenu");
    }
}
