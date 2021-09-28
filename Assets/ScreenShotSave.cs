using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotSave : MonoBehaviour
{

    BoxCollider myColl;

    private void Awake()
    {
        myColl = GetComponent<BoxCollider>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("Player pass checkpoint");

            myColl.enabled = false;

            string picName = "pic" + LevelManager.Instance.CurrentLevel;
            CMScreenshot.Instance.TakeScreenshot(picName);


        }
    }

    
}
