using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{

    public GameObject fish;
    private bool timecheck;
    private float t;
    private GameObject player;
    private Vector3 pos;
    public GameObject water;
    private bool ac = false;

    [SerializeField] float currentLvlNube;
    

    LevelManager manager;
    Animator _waterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _waterAnimator = water.GetComponent<Animator>();
       // _waterAnimator.SetBool("SecondWaterAnimation", false);
        t = 3f;
        timecheck = false;
        player = GameObject.FindGameObjectWithTag("Player");
        pos.Set(player.transform.position.x, this.transform.position.y, -0.52f);
        //manager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        manager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.CurrentLevel == currentLvlNube)
        {
            if (timecheck)
            {
                t -= Time.deltaTime;
            }

            if (t <= 0)
            {
                Fishjump();
            }
        }
        pos.Set(player.transform.position.x, player.transform.position.y, -0.52f);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         if(currentLvlNube == manager.CurrentLevel)
    //         {
    //             timecheck = true;
    //         }
            
    //     }
    // }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            timecheck = false;
            t = 3f;
        }

       
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
           
            if (currentLvlNube == manager.CurrentLevel)
            {

                timecheck = true;
            }

            // Active();
        }

        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
          
            timecheck = false;
            t = 3f;
            Active();
        }
       
    }

    public void Fishjump()
    {
        GameObject player = Instantiate(fish, pos , Quaternion.identity) as GameObject;
       // float speed = 200;
        Vector3 force = transform.forward;
        force = new Vector3(0, force.y, 0);
       // player.GetComponent<Rigidbody>().AddForce(force * speed);
        t = 3f;
        timecheck = false;
    }

    void Active()
    {
        if (ac == false)
        {
            
          
            water.SetActive(true);
            ac = true;
        }
    }

    

}
