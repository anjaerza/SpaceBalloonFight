using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ChangeMaterialBeach : MonoBehaviour
{
    [SerializeField] Material[] materialEnemie;
   Renderer [] render;
   private EnemieType enemyTipe;


    private void Start() {
        enemyTipe=GetComponentInParent<EnemieType>();
        render=GetComponentsInChildren<Renderer>();
        BeginEnemieMaterialType2();
    }

    public void BeginEnemieMaterialType2(){



        switch (enemyTipe._enemies){
            case Eenemie.Basico:
            foreach (var myRnder in render)
            {
            myRnder.enabled=true;
            myRnder.sharedMaterial=materialEnemie[0];
            }
           

            break;
            case Eenemie.Medio:
             foreach (var myRnder in render)
            {
            myRnder.enabled=true;
            myRnder.sharedMaterial=materialEnemie[1];
            }

            break;
            case Eenemie.Alto:
              foreach (var myRnder in render)
            {
            myRnder.enabled=true;
            myRnder.sharedMaterial=materialEnemie[2];
            }

            break;

        }
        

    }
}
