using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] string level;
    Transform player;
    BoxCollider col;
    float deadZone;
    CinemachineFramingTransposer vcamBody;
    bool pass = false;

    [SerializeField] CinemachineVirtualCamera vcam;
    LevelManager gManager;

    EnemiIA enemiIA;

    public bool Pass { get => pass; set => pass = value; }
    public string Level { get => level; set => level = value; }

    private AudioSource aud;
    [SerializeField] ParticleSystem parSys;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        col = GetComponent<BoxCollider>();
        // vcamBody = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        // deadZone = vcamBody.m_DeadZoneHeight;
        // gManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        gManager = LevelManager.Instance;
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        DistanceCheck();
    }

    void DistanceCheck()
    {
        if (player.transform.position.y > transform.position.y + 1f /*&& Pass==false*/)
        {
            col.enabled = true;
            Pass = true;
            //Debug.Log("Player Passed");
            gManager.CurrentLevel++;
            GameObject.Find("Arrow").GetComponent<Animator>().ResetTrigger("Arrow");

            aud.Play();
            parSys.Play();
        }
        else if (player.transform.position.y < transform.position.y)
        {
            if (EnemyCount(Level) == 0)
            {
                col.enabled = false;
                // vcamBody.m_DeadZoneHeight = deadZone;
                //Debug.Log("No ENEMIES");

                if (Pass == false)
                {
                    GameObject.Find("Arrow").GetComponent<Animator>().SetTrigger("Arrow");
                }
            }
            else
            {
                //Debug.Log(EnemyCount(Level) + "Enemies");
                if (transform.position.y - player.transform.position.y < 7)
                {
                    // vcamBody.m_DeadZoneHeight = 2;
                    //Debug.Log("Close to top");
                }
                else
                {
                    //Debug.Log("Far from top");
                    // vcamBody.m_DeadZoneHeight = deadZone;
                }
            }
        }
    }

    int EnemyCount(string level)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(level);

        return enemies.Length;
    }
}