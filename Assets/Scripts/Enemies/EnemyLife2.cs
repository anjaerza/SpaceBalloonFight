using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyLife2 : MonoBehaviour
{
    private EnemyLife enemyLife;
    [SerializeField] private GameObject collider;
    void Start()
    {
        enemyLife = GetComponentInParent<EnemyLife>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyLife.TakeDamage();
            collider.SetActive(false);
        }
    }
}
