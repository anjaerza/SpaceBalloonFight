using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Checkpoint finalCheckpoint;
    [SerializeField] string winScene;
    [SerializeField] string lossScene;
    [SerializeField] PlayerLife vidaJugador;
    [SerializeField] int currentLevel = 1;
    [SerializeField] Checkpoint[] checkpoints;
    [SerializeField] Transform[] spawners;
    [SerializeField] GameObject player;


    Vector3 spawnPoint;
    private AudioSource myAudio;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public Vector3 SpawnPoint { get => spawnPoint; set => spawnPoint = value; }
    public Transform[] Spawners { get => spawners; set => spawners = value; }

    /// <summary>
    /// //con esto no deserealiza los componentes
    /// </summary>
    protected override void Awake()
    {
        //si se necesita asignar algo en el awake, hacerlo aqui dentro.

        GuardadoSerializado.Instance.Guardar(1, 0f, -12.22f, -0.52f);
    }

    private void Start()
    {
        Invoke("PostStart", 1f);
        // PostStart();
    }

    private void PostStart()
    {
        SpawnPoint = GameManager.Instance.PosInicial;

        if (GameManager.Instance.CurrentLevelGameManager > 1)
        {
            CurrentLevel = GameManager.Instance.CurrentLevelGameManager - 1;
        }
        else
        {
            CurrentLevel = GameManager.Instance.CurrentLevelGameManager;
        }
        // currentLevel=GameManager.Instance.CurrentLevelGameManager;

        if (player != null)
        {
            //UpdatePlayerPos(SpawnPoint);

            
            vidaJugador.Resurrection();
            vidaJugador.Lives++;
            vidaJugador.LivesImages[0].SetActive(true);
          
        }
    }

    public void UpdatePlayerPos(Vector3 newVec)
    {

        //Se hizo este condicional para corregir bug al elegir level 2
        // if(GameManager.Instance.CurrentLevelGameManager==2){
        //     player.transform.position= new Vector3(-4.1f,14.87f,-0.52f);
        // }else{
        //     player.transform.position = newVec;
        // }

        Vector3 myVector2 = new Vector3(newVec.x, newVec.y, -0.52f);

        player.transform.position = myVector2;


    }

    void Update()
    {
        if (finalCheckpoint.Pass == true)
        {
            UIController.Instance.Score = 0;

            SceneManager.LoadScene(winScene);
        }
        else if (vidaJugador.Lives == 0)
        {
            UIController.Instance.Score = 0;
            myAudio = GetComponent<AudioSource>();
            GameManager.Instance.LastLevel = CurrentLevel;
            // SceneManager.LoadScene("GameOver");
            myAudio.Play();
            // MOD for player prefs
            // PlayerPrefs.SetInt("CurrentLevel",currentLevel);  
        }

        //GameManager.Instance.LastLevel = CurrentLevel;

        CheckPointManager();
    }

    void CheckPointManager()
    {
        if (SpawnPoint == null || SpawnPoint != spawners[CurrentLevel - 1].position)
        {
            SpawnPoint = spawners[CurrentLevel - 1].position;
        }

        foreach (Checkpoint check in checkpoints)
        {
            if (int.Parse(check.Level) == CurrentLevel)
            {
                if (check.enabled == false)
                {
                    check.enabled = true;
                }
            }
            else
            {
                if (check.enabled == true)
                {
                    check.enabled = false;
                }
            }
        }
    }
}
