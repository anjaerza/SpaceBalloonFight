using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private float time;
    public AudioClip audio;


    // Start is called before the first frame update
    void Start()
    {
        time = 2f;
        AudioSource source = GetComponent<AudioSource>();

        source.clip = audio;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           

            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);


       /* Destroy(this.gameObject);
        StopCoroutine(Die());*/
    }
}
