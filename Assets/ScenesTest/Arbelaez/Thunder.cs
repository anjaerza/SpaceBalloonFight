using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{

    [SerializeField] float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 15;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        Vector3 force = transform.position;
       
        
        force = new Vector3(-force.x, -force.y, 0);
        transform.rotation = Quaternion.Inverse(transform.rotation);
        GetComponent<Rigidbody>().AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 force = transform.position;
        force = new Vector3(-force.x, -force.y, 0);
        GetComponent<Rigidbody>().AddForce(force * 1);
    }
}
