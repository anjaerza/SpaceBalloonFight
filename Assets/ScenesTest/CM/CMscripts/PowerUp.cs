using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType type;
    [SerializeField] float timeEffect, timeSpawned;
    //[SerializeField] bool isPickUp;
    Coroutine coroutineA;
    private AudioSource aud;

    private void Start()
    {
        coroutineA = StartCoroutine(ReleasePowerUp(timeSpawned));
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectPowerUp(other.gameObject);
            aud.Play();
        }
    }

    private void CollectPowerUp(GameObject player)
    {
        //isPickUp = true;

        //StopAllCoroutines(); //WARNING: STOP ALL THE COROUTINES IN THE GAME?..........

        StopCoroutine(coroutineA);

        switch (type)
        {
            case PowerUpType.Shield:
                Shield(player);
                break;
            case PowerUpType.Balloon:
                Balloon(player);
                break;
            case PowerUpType.Slow:
                Slow();
                break;
            default:
                break;
        }

        //Diasable PowerUp
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        StartCoroutine(ReleasePowerUp(timeEffect));
    }

    private void Slow()
    {
        //Get all enemies
        EnemiIA[] allEnemies = GameObject.FindObjectsOfType<EnemiIA>();

        //Set low speed
        foreach (EnemiIA enemy in allEnemies)
        {
            enemy.Speed = 200f;
        }

        //reset enemy speed after x seconds
        StartCoroutine(SlowTime(allEnemies, timeEffect));
    }

    private IEnumerator SlowTime(EnemiIA[] allEnemies, float time)
    {
        yield return new WaitForSeconds(time);

        foreach (EnemiIA enemy in allEnemies)
        {
            enemy.Speed = 900;
        }
    }

    private void Balloon(GameObject player)
    {
        //Aditional Life
        if (player.GetComponentInChildren<PlayerLife>().Lifes % 2 != 0) //Odd
        {
            player.GetComponentInChildren<PlayerLife>().Lifes++;
        }
    }

    private void Shield(GameObject player)
    {
        //Inmunity for 5 seconds
        player.GetComponentInChildren<PlayerLife>().IsShield = true;

        //diasable shield after x seconds
        StartCoroutine(ShieldTime(player, timeEffect));
    }

    private IEnumerator ShieldTime(GameObject player, float time)
    {
        yield return new WaitForSeconds(time);

        player.GetComponentInChildren<PlayerLife>().IsShield = false;
    }

    private IEnumerator ReleasePowerUp(float time)
    {
        yield return new WaitForSeconds(time);

        //Pool?...
        Destroy(gameObject);
    }
}
