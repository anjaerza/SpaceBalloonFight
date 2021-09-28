using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class EnemieType : MonoBehaviour
{
    [SerializeField] public Eenemie _enemies;
    private EnemiIA enemiIA;
    private float speed;
    //[SerializeField]private int level;

    //public int Level { get => level; set => level = value; }

    void MakeTheEnemie(){
        switch (_enemies)
        {
            case Eenemie.Basico:
            enemiIA.DistanceToActiveFindPlayer=6f;
            enemiIA.Speed=700f;
            enemiIA.NextWayPointDistance=1f;
            //print("este es el basico");
            break;
            case Eenemie.Medio:
            enemiIA.DistanceToActiveFindPlayer=8f;
            enemiIA.Speed=800f;
            enemiIA.NextWayPointDistance=2f;
            //print("Este es el medio");
            break;
            case Eenemie.Alto:
            enemiIA.DistanceToActiveFindPlayer=10f;
            enemiIA.Speed=900f;
            enemiIA.NextWayPointDistance=3f;
            //print("este es el alto");
            break;
            
            
        }
    }    

    private void Start() {
        BeginEnemie();
       
    }

    public void _typeEnemi(Eenemie enemie){

        enemie=_enemies;
    }


    public void BeginEnemie(){
        enemiIA=GetComponent<EnemiIA>();
        MakeTheEnemie();
    }


}
