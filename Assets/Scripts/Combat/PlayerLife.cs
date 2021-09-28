using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PlayerLife : MonoBehaviour
{
    //CM Mods
    [SerializeField] GameObject cmCam;
    [SerializeField] bool isShield = false;
    public bool IsShield { get => isShield; set => isShield = value; }

    private EnemiIA enemyIA;

    [SerializeField] ParticleSystem poof;
    [SerializeField] ParticleSystem lightning;
    TouchMovement tchMovement;

    //CM Mods



    [SerializeField] private int lifes = 2;
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject padre;
    [SerializeField] private GameObject[] livesImages = new GameObject[2];
    private AudioSource aud;


    private Quaternion rotGlobo2;


    //Andres
    LevelManager manager;

    public int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }
    public GameObject[] LivesImages{
        get{return livesImages;}
        set{livesImages=value;}           
        
    }
    private Vector3 spawn;
    // private Vector3 life2Pos;


    void Start()
    {
        //manager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        manager = LevelManager.Instance;
        spawn = manager.SpawnPoint;
        rotGlobo2 = life2.transform.rotation;
        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyIA = other.gameObject.GetComponentInParent<EnemiIA>();
            //CM Mods
            if (!isShield && enemyIA.isStoppedIA == false)
            {
                TakeDamage();
                poof.Play();

                if (GetComponentInParent<CMScreenWrapRF>() != null)
                {
                    if (GetComponentInParent<CMScreenWrapRF>().advancedWrapping)
                    {
                        GetComponentInParent<CMScreenWrapRF>().ResetGhosts();
                    }
                }

                if (GetComponentInParent<TouchMovement>() != null)
                {
                    GetComponentInParent<TouchMovement>().ChangeForceDrag();
                }
            }
        }
        else if (other.gameObject.tag == "Rayo")
        {
            lightning.Play();
            Resurrection();
        }
        else if (other.gameObject.tag == "Fish")
        {
            tchMovement = gameObject.GetComponentInParent<TouchMovement>();
            tchMovement.enabled = false;
            StartCoroutine(WaitForResurrectionFish(1f));
        }

        if (lifes == 0)
        {

            life2.transform.position = new Vector3(padre.transform.position.x + 0.4f, padre.transform.position.y - 0.3f, padre.transform.position.z);
            life2.transform.rotation = rotGlobo2;



            // Resurrection();

            StartCoroutine(WaitForResurrection(0.2f));
        }
    }


    void TakeDamage()
    {
        lifes--;
        isShield = true;
        StartCoroutine(ShieldTimeAfterTakeDamage(1.2f));
        aud.Play();


        if (lifes == 1)
        {
            life1.SetActive(false);
            life2.transform.position = new Vector3(padre.transform.position.x + 0.15f, padre.transform.position.y, padre.transform.position.z);
            life2.transform.rotation = Quaternion.identity;
        }

        //CM Mods

        //CM Mods
    }

    public void Resurrection()
    {
        player.transform.position = manager.SpawnPoint;

        var newPos = new Vector3(player.transform.position.x, player.transform.position.y, -0.52f);
        player.transform.position = newPos;

        lives -= 1;
        lifes = 2;
        life1.SetActive(true);

        if (lives == 2)
        {
            livesImages[0].SetActive(false);
        }
        if (lives == 1)
        {
            livesImages[1].SetActive(false);
        }
        if(lives == 0)
        {
            //  CMSavePlayerPrefs.Instance.SavePlayerPrefs();   
            PlayerPrefs.SetFloat("score", UIController.Instance.Score);
            //print(UIController.Instance.Score);

            //CMPlayFabLogin.Instance.SubmitScore(UIController.Instance.Score);

             CMPlayFabController.Instance.StartCloudUpdatePlayerScore();

            SceneManager.LoadScene("GameOver");
        }
        // player.transform.position = spawn;
        // lives = 1;
        // lifes = 2;
        // life1.SetActive(true);

        //CM Mods
        if (GetComponentInParent<CMScreenWrapRF>() != null)
        {
            if (GetComponentInParent<CMScreenWrapRF>().advancedWrapping)
            {
                GetComponentInParent<CMScreenWrapRF>().ResetGhosts();
            }
        }

        if (GetComponentInParent<TouchMovement>() != null)
        {
            GetComponentInParent<TouchMovement>().ChangeForceDrag();
        }
    }
    private IEnumerator ShieldTimeAfterTakeDamage(float time)
    {
        yield return new WaitForSeconds(time);

        IsShield = false;
    }

    private IEnumerator WaitForResurrection(float time)
    {
        yield return new WaitForSeconds(time);
        Resurrection();
    }

    private IEnumerator WaitForResurrectionFish(float time)
    {
        yield return new WaitForSeconds(time);
        Resurrection();
        tchMovement.enabled = true;
    }
}