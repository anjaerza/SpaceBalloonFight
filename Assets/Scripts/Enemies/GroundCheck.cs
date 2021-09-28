using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GroundCheck : MonoBehaviour
{
	public bool isGrounded { get; set; }
    [SerializeField] private float timeToResurrection=3f;
    EnemyLife enemyLife;
    EnemyHurt enemyHurt;
    EnemiIA enemiIA;
    // private AudioSource aud;
    private void Start() {
        enemyHurt=GetComponentInParent<EnemyHurt>();
        enemyLife=GetComponentInParent<EnemyLife>();
        enemiIA=GetComponentInParent<EnemiIA>();
        // aud = GetComponent<AudioSource>();
    }   


    private void OnTriggerEnter(Collider other) 
    {        


      if(other.gameObject.layer==8)
      {
        isGrounded=true;
        enemiIA.animParachute.SetBool("ActiveParachute",false);
        enemiIA.sprParachute.enabled=false;
        // aud.Play();

        //print("Grounded");
      }
      if(isGrounded&&enemyLife.Lifes==1)
      {
          StartCoroutine(counterResurrection());           
      }

    }

    private void OnTriggerExit(Collider other) {

      if(other.gameObject.layer==8)
      {
        isGrounded=false;
        //print("Grounded out");
      }
    
    }

      IEnumerator counterResurrection()
      {
          yield return new WaitForSeconds (timeToResurrection);
          enemyHurt.enemyResurrection();
      }
 
}