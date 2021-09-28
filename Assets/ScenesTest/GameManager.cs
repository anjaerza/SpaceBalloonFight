using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int currentLevelGameManager = 1;
    [SerializeField] Vector3 posInicial = new Vector3();
    [SerializeField] int lastLevel = 1;

    private bool IsactiveSplash=true;

    public int CurrentLevelGameManager { get => currentLevelGameManager; set => currentLevelGameManager = value; }
    public Vector3 PosInicial { get => posInicial; set => posInicial = value; }
    public int LastLevel { get => lastLevel; set => lastLevel = value; }
    public bool IsactiveSplash1 { get => IsactiveSplash; set => IsactiveSplash = value; }

    //protected override void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    public void PleaseAwake()
    {
        print("Awakening");
    }
}