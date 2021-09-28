using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerale : MonoBehaviour
{
    public GameObject spawn;

    public GameObject projectile;

    private float time;

    private float reloadtime;

    private float counter;
    [SerializeField] private float currentLvlNube;

    LevelManager manager;



    [SerializeField] ParticleSystem parSys;


    // Start is called before the first frame update
    void Start()
    {
        parSys=GameObject.Find("rayoparticles").GetComponent<ParticleSystem>();
        // parSys=GetComponent<ParticleSystem>();
        time = 5f;
        counter = 3f;
        reloadtime = 10f;
        //manager=GameObject.Find("GameManager").GetComponent<LevelManager>();
        manager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLvlNube == manager.CurrentLevel)
        {

            time -= Time.deltaTime;
        }

       
        

        if(time <= 0 && counter > 0)
        {
            Fire();
            //StartCoroutine(Firei());
            // return;
        }
        else if(counter <= 0 )
        {
            Debug.Log("No es posible disparar mas");
            reloadtime -= Time.deltaTime;
        }

        if(reloadtime <= 0)
        {
            reloadtime = 50f;
            counter = 3f;
        }

    }

    void Fire()
    {
        transform.Rotate(0, 0, Random.Range(-360, 360));
        GameObject spawn = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        transform.eulerAngles = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
        float speed = 80;
        Vector3 force = transform.forward;
        force = new Vector3(force.x, force.y, 0);
        spawn.GetComponent<Rigidbody>().AddForce(force * speed);
        spawn.transform.forward = transform.forward;
        time = 30f;
        counter -= 1;
        parSys.Play();
    }

    /*IEnumerator Firei()
    {
        transform.eulerAngles.z
        transform.eulerAngles.x
        force.x
        yield return new WaitForSeconds(0.2f);
    }*/
}
