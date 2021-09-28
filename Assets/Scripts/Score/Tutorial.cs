using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject btn ;
    [SerializeField] GameObject animFirstAgua;

    Animator _animFirstAgua;
     void Awake()
    {
        _animFirstAgua = animFirstAgua.GetComponent<Animator>();

        _animFirstAgua.SetBool("FirstWaterAnimation", false);
        panel.SetActive(true);
        StartCoroutine(Esperar());


    }
    
    public void Continuar()
    {
       
        Time.timeScale = 1;
        StartCoroutine(esperarPorAnimacionAgua());
    }
    IEnumerator esperarPorAnimacionAgua()
    {
        yield return new WaitForSeconds(0.1f);
        _animFirstAgua.SetBool("FirstWaterAnimation", true);
        StartCoroutine(WaitToPutFalse());
    }
    IEnumerator Esperar()
    {
        
        yield return new  WaitForSeconds(1.2f);   
        Time.timeScale = 0;     
        btn.SetActive(true);
       
    }

    IEnumerator WaitToPutFalse()
    {
        yield return new WaitForSeconds(3f);
        _animFirstAgua.SetBool("FirstWaterAnimation", false);
    }

    
}
