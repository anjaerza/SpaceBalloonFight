using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    float repulsionForce = 5f;
    Rigidbody rb;

    //Vector3 rayDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(repulsionForce * direction.normalized, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            rb = other.gameObject.GetComponentInParent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(repulsionForce * direction.normalized, ForceMode.Impulse);
        }
    }
}
