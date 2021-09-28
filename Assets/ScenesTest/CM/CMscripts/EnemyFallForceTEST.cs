using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallForceTEST : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            EnemyFallForce eff = other.gameObject.GetComponent<EnemyFallForce>();
            eff.Direction = direction;
            eff.enabled = true;
            //print(direction);
        }
    }
}