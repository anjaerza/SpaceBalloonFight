using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveSplashScreen : MonoBehaviour
{
    [SerializeField]
    GameObject panelTrasero;
    private void Start()
    {
        if (GameManager.Instance.IsactiveSplash1 == true)
        {
            gameObject.SetActive(true);
            panelTrasero.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            panelTrasero.SetActive(false);
        }
    }
}
