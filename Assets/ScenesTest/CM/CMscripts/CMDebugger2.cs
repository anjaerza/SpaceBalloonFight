using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMDebugger2 : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    void Update()
    {
        GetComponent<Text>().text = "Current LevelManager: " + levelManager.CurrentLevel + ", Current GameManager: " + GameManager.Instance.CurrentLevelGameManager;
    }
}