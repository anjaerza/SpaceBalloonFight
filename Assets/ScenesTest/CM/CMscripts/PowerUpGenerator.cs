using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpGenerator : MonoBehaviour
{
    float height, width;

    [SerializeField] float correctZPosition = -0.52f;

    //[SerializeField] GameObject[] PowerUpsPrefabs;

    [SerializeField] GameObject shieldPowerUpPrefab, balloonPowerUpPrefab, slowPowerUpPrefab;

    [SerializeField] float shieldPowerUpSpawnRate, balloonPowerUpSpawnRate, slowPowerUpSpawnRate;

    private void Start()
    {
        height = Screen.height;
        width = Screen.width;
        
        StartCoroutine(SpawnShieldPowerUp(shieldPowerUpSpawnRate));
        StartCoroutine(SpawnBalloonPowerUp(balloonPowerUpSpawnRate));
        StartCoroutine(SpawnSlowPowerUp(slowPowerUpSpawnRate));
    }

    private IEnumerator SpawnShieldPowerUp(float spawnRate)
    {
        float rx = Random.Range(0f, width);
        float ry = Random.Range(0f, height);

        Vector3 randomScreenPos = new Vector3(rx, ry, 0f);

        Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);

        //SET CORRECT Z POSITION
        inWorldPos.z = correctZPosition;

        GameObject myGameObject = Instantiate(shieldPowerUpPrefab, inWorldPos, Quaternion.identity, transform);

        Collider[] myColls = Physics.OverlapSphere(myGameObject.transform.position, myGameObject.GetComponent<SphereCollider>().radius);

        if (myColls.Length > 1)
        {
            Destroy(myGameObject);
            StartCoroutine(SpawnShieldPowerUp(spawnRate));
        }
        else
        {
            yield return new WaitForSeconds(spawnRate);
            StartCoroutine(SpawnShieldPowerUp(spawnRate));
        }
    }

    private IEnumerator SpawnBalloonPowerUp(float spawnRate)
    {
        float rx = Random.Range(0f, width);
        float ry = Random.Range(0f, height);

        Vector3 randomScreenPos = new Vector3(rx, ry, 0f);

        Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);

        //SET CORRECT Z POSITION
        inWorldPos.z = correctZPosition;

        PlayerLife pl = GameObject.FindObjectOfType<PlayerLife>();
        if (pl != null && pl.Lifes % 2 != 0) //Odd
        {
            GameObject myGameObject = Instantiate(balloonPowerUpPrefab, inWorldPos, Quaternion.identity, transform);

            Collider[] myColls = Physics.OverlapSphere(myGameObject.transform.position, myGameObject.GetComponent<SphereCollider>().radius);

            if (myColls.Length > 1)
            {
                Destroy(myGameObject);
                StartCoroutine(SpawnBalloonPowerUp(spawnRate));
            }
            else
            {
                yield return new WaitForSeconds(spawnRate);
                StartCoroutine(SpawnBalloonPowerUp(spawnRate));
            }
        }
    }

    private IEnumerator SpawnSlowPowerUp(float spawnRate)
    {
        float rx = Random.Range(0f, width);
        float ry = Random.Range(0f, height);

        Vector3 randomScreenPos = new Vector3(rx, ry, 0f);

        Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);

        //SET CORRECT Z POSITION
        inWorldPos.z = correctZPosition;

        //KillCount or ScoreCount?....

        //if (kills >= 2 || score >= 100)
        //{
            //GameObject myGameObject = Instantiate(balloonPowerUpPrefab, inWorldPos, Quaternion.identity, transform);

            //Collider[] myColls = Physics.OverlapSphere(myGameObject.transform.position, myGameObject.GetComponent<SphereCollider>().radius);

            //if (myColls.Length > 1)
            //{
            //    Destroy(myGameObject);
            //    StartCoroutine(SpawnBalloonPowerUp(spawnRate));
            //}
            //else
            //{
            //    yield return new WaitForSeconds(spawnRate);
            //    StartCoroutine(SpawnBalloonPowerUp(spawnRate));
            //}
        //}
        yield return null;
    }
}
