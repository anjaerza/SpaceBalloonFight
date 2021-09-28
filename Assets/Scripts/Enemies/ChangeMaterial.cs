using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ChangeMaterial : MonoBehaviour
{
   [SerializeField] Material[] materialEnemie;
   Renderer render;
   private EnemieType enemyTipe;


    private void Start() {
        enemyTipe=GetComponentInParent<EnemieType>();
        render=GetComponent<Renderer>();
        BeginEnemieMaterialType();
    }

    public void BeginEnemieMaterialType(){



        switch (enemyTipe._enemies){
            case Eenemie.Basico:
            render.enabled=true;
            render.sharedMaterial=materialEnemie[0];

            break;
            case Eenemie.Medio:
            render.enabled=true;
            render.sharedMaterial=materialEnemie[1];

            break;
            case Eenemie.Alto:
             render.enabled=true;
            render.sharedMaterial=materialEnemie[2];

            break;

        }
        

    }




}
