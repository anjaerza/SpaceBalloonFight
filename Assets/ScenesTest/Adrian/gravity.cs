using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gravity : MonoBehaviour
{
    [SerializeField]Transform center;
    [SerializeField]float radius;
    [SerializeField]float rotationSpeed;
    [SerializeField] float factor;
    [SerializeField] Collider[] objectsInRange;
    [SerializeField] float umbral;
    float g ;
    float m1;
    // Start is called before the first frame update
    void Start()
    {
        center = center.gameObject.transform;
        objectsInRange = Physics.OverlapSphere(center.position, radius);
        g = 6.67408f * factor;
        m1 = center.GetComponent<Rigidbody>().mass;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward * rotationSpeed);
        foreach (Collider objectInVicinity in objectsInRange)
        {
            if (objectInVicinity == null)
            {

                break;
            }
            if (objectInVicinity.gameObject.GetComponent<Rigidbody>() == null)
            {

                objectInVicinity.gameObject.AddComponent<Rigidbody>();
            }

            float r = (center.position - objectInVicinity.transform.position).magnitude;
            float m2 = objectInVicinity.gameObject.GetComponent<Rigidbody>().mass;

            if (r > umbral)
            {
               

              


                Vector3 direction = center.position - objectInVicinity.transform.position;
                objectInVicinity.GetComponent<Rigidbody>().AddForce(direction * (g * ((m2 * m1) /(r*r))), ForceMode.Impulse);
                objectInVicinity.transform.Rotate(transform.forward*rotationSpeed);
                objectInVicinity.GetComponent<Rigidbody>().isKinematic = false;

            }
            else
            {
                objectInVicinity.GetComponent<Rigidbody>().isKinematic = true;
            }
            







        }

    }
}
