using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using Cinemachine;
[RequireComponent(typeof(AudioSource))]
public class EnemyLife : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cmVirtualCameraNOISE;



    [SerializeField] private int lifes;
    [SerializeField] ParticleSystem pop;
    private bool ballTrue = true;
    
    private EnemyHurt enemyHurt;
    private EnemieType enemyType;
    private UIController uiController;
    private GroundCheck groundCheck;
    private AudioSource aud;
    [SerializeField] AudioClip [] clips; 

    public int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    public bool BallTrue
    {
        get { return ballTrue; }
        set { ballTrue = value; }
    }

    [SerializeField] private GameObject enemy;
    [SerializeField] public GameObject ball;
    // Carlos Mod
    private EnemiIA enemiIA;

    private void Start()
    {
        enemyHurt = GetComponentInParent<EnemyHurt>();
        enemyType = GetComponentInParent<EnemieType>();
        uiController = FindObjectOfType<UIController>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        ballTrue = true;
        // Carlos Mod
        enemiIA=GetComponentInParent<EnemiIA>();
        aud=GetComponent<AudioSource>();



    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemiIA.sprParachute.enabled=true;
            enemiIA.animParachute.SetBool("ActiveParachute",true);                
            TakeDamage();       
        }                       
    }    

    public void TakeDamage()
    {
        lifes -= 1;
        ball.SetActive(false);
        enemyHurt.enemyHurtFalling();
        aud.clip=clips[0];
        pop.Play();
        aud.Play();             
        
            switch (enemyType._enemies)
            {
                case Eenemie.Basico:
                    if (ballTrue)
                    {
                        uiController.AñadirPuntaje(500);
                        ActiveNoise();
                        
                    }
                    else
                    {
                        if(groundCheck.isGrounded == false) uiController.AñadirPuntaje(1000);
                        else uiController.AñadirPuntaje(750);                                                           
                        ActiveNoise();
                        Dead();
                    }
                    break;
                case Eenemie.Medio:
                    if (ballTrue)
                    {
                        uiController.AñadirPuntaje(750);    
                        ActiveNoise();                   
                    }
                    else
                    {
                        if (groundCheck.isGrounded == false) uiController.AñadirPuntaje(1500);
                        else uiController.AñadirPuntaje(1000);
                        ActiveNoise();
                        Dead();
                    }
                    break;
                case Eenemie.Alto:
                    if (ballTrue)
                    {
                        uiController.AñadirPuntaje(1000);   
                        ActiveNoise();                     
                    }
                    else
                    {
                        if (groundCheck.isGrounded == false) uiController.AñadirPuntaje(2000);
                        else uiController.AñadirPuntaje(1500);
                        ActiveNoise();                    
                        Dead();
                    }
                    break;

            }                    

            ballTrue = false;            
        
        /*else
        {
            switch (enemyType._enemies)
            {
                case Eenemie.Basico:
                    uiController.AñadirPuntaje(750);
                    ActiveNoise();
                    Dead();
                    break;
                case Eenemie.Medio:
                    uiController.AñadirPuntaje(1000);
                    ActiveNoise();
                    Dead();
                    break;
                case Eenemie.Alto:
                    uiController.AñadirPuntaje(1500);
                    ActiveNoise();
                    Dead();
                    break;
            }
        }*/

        uiController.ShowScore(enemy);
    }
    public void Dead()
    {
        GameObject enemyGoTaged = enemy.transform.GetChild(0).gameObject;
        enemyGoTaged.SetActive(false);
        aud.clip=clips[1];
        aud.Play();     
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(DestroyAfterShowScore());
        enemiIA.sprParachute.enabled=false;
    }

    private IEnumerator DestroyAfterShowScore()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(enemy);
    }
    private void ActiveNoise(){

         cmVirtualCameraNOISE.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain= 20f;
         cmVirtualCameraNOISE.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain= 5f;

        StartCoroutine(UnactiveNoise());
    }

    private IEnumerator UnactiveNoise(){

        yield return new WaitForSeconds(0.3f);
         cmVirtualCameraNOISE.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0f;

        cmVirtualCameraNOISE.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain= 0f;

    }

    private IEnumerator WaitForParticles(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
