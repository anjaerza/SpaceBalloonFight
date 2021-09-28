using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPointGameComplete :MonoBehaviour
{

    private LevelManager manager;
    // Manager manager;
    private void Start() {
        manager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
    }


  /* private void OnTriggerEnter(Collider other) {
       if(other.CompareTag("Player")){

           manager.lastChekPointToReturn=transform.position;


                  
       }
       
   }*/
}
