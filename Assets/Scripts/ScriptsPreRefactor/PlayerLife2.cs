using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerLife2 : MonoBehaviour
{
    [SerializeField] private bool isShield = false;

    [SerializeField] private int lifes = 2;    
    [SerializeField] private int lives = 3;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject padre;

    [SerializeField] private GameObject[] livesImages = new GameObject[2];

    private EnemiIA enemyIA;

    private AudioSource aud;

    private Quaternion rotGlobo2;

    private LevelManager manager;

    private Vector3 spawn;

    public int Lifes { get { return lifes; } set { lifes = value; } }
    public int Lives { get { return lives; } set { lives = value; } }
    public bool IsShield { get => isShield; set => isShield = value; }

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        spawn = manager.SpawnPoint;

        rotGlobo2=life2.transform.rotation;

        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIA = other.gameObject.GetComponentInParent<EnemiIA>();
            if (!isShield && enemyIA.isStoppedIA == false)
            {
                TakeDamage();
                //GetComponentInParent<TouchMovementRF>().ChangeForceDrag();
                //GetComponentInParent<CMScreenWrapRF>().UpdateRenderers();
                GetComponentInParent<CMScreenWrapRF>().ResetGhosts();
            }
        }
        else if(other.gameObject.CompareTag("Rayo"))
        {
            Resurrection();
        }

        if (lifes == 0 )
        {
            life2.transform.position = new Vector3(padre.transform.position.x+0.4f, padre.transform.position.y-0.3f, padre.transform.position.z);
            life2.transform.rotation = rotGlobo2;
            Resurrection();
        }        
    }

    void TakeDamage()
    {
        lifes--;
        aud.Play();

        if(lifes == 1)
        {            
            life1.SetActive(false);
            life2.transform.position = new Vector3(padre.transform.position.x+0.15f, padre.transform.position.y, padre.transform.position.z);
            life2.transform.rotation = Quaternion.identity;
        }
    }

    public void Resurrection()
    {
    	player.transform.position = manager.SpawnPoint;
        lives -= 1;
        lifes = 2;
        life1.SetActive(true);

        //if(lives > 1)
        //{
        //	livesImages[0].SetActive(false);
        //}
        //else
        //{
        //	livesImages[1].SetActive(false);
        //}
    }
}
