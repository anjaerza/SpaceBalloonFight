using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSaveLoad : MonoBehaviour
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

            GuardadoSerializado.Instance.Guardar(LevelManager.Instance.CurrentLevel, 0f, LevelManager.Instance.SpawnPoint.y, -0.52f);
            GuardadoSerializado.Instance.GuardarLastLevel(LevelManager.Instance.CurrentLevel);


        }
    }
}
