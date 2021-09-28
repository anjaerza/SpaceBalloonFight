using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI puntaje;
    [SerializeField] TextMeshProUGUI nombre;

    private void Start()
    {
        ActualizarUIGameOver();
    }

    void ActualizarUIGameOver()
    {
        puntaje.text = PlayerPrefs.GetFloat("score").ToString();
        nombre.text = PlayerPrefs.GetString("nickname");
    }
}
