using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Enums;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] private float gravityTotal = 0f;
    [SerializeField] private float timeToResurrection = 3f;

    EnemieType enemieType;

    private EnemiIA enemiIA;
    private EnemyLife enemyLife;
    private Rigidbody rbEnemy, rbenemyGFX;
    private ChangeMaterial changeMaterial;
    private ChangeMaterialBeach changeMaterialBeach;
    [SerializeField] private GameObject ChangeMaterialParentBeach;
    [SerializeField] private GameObject ParachuteLife2;

    private EnemyLife2 enemyLife2;

    private void Start()
    {
        enemyLife = GetComponentInChildren<EnemyLife>();
        enemyLife2 = GetComponentInChildren<EnemyLife2>();
        rbEnemy = GetComponent<Rigidbody>();
        enemieType = GetComponent<EnemieType>();
        changeMaterial = GetComponentInChildren<ChangeMaterial>();

        if (ChangeMaterialParentBeach != null && ChangeMaterialParentBeach.name == "meshenemigoplaya")
        {
            changeMaterialBeach = GetComponentInChildren<ChangeMaterialBeach>();
        }
        else
        {
            changeMaterialBeach = null;
        }
    }

    public void enemyResurrection()
    {
        enemiIA.isStoppedIA = false;
        enemyLife.Lifes++;
        enemyLife.ball.SetActive(true);
        enemyLife.BallTrue = true;

        if (ParachuteLife2 != null)
        {
            ParachuteLife2.SetActive(false);
        }

        switch (enemieType._enemies)
        {
            case Eenemie.Basico:
                enemieType._enemies = Eenemie.Medio;
                enemieType.BeginEnemie();
                if (ChangeMaterialParentBeach != null && ChangeMaterialParentBeach.name == "meshenemigoplaya")
                {
                    changeMaterialBeach.BeginEnemieMaterialType2();
                }
                else
                {
                    changeMaterial.BeginEnemieMaterialType();
                }
                break;

            case Eenemie.Medio:
                enemieType._enemies = Eenemie.Alto;
                enemieType.BeginEnemie();
                if (ChangeMaterialParentBeach != null && ChangeMaterialParentBeach.name == "meshenemigoplaya")
                {
                    changeMaterialBeach.BeginEnemieMaterialType2();
                }
                else
                {
                    changeMaterial.BeginEnemieMaterialType();
                }
                break;

            case Eenemie.Alto:
                enemieType._enemies = Eenemie.Alto;
                enemieType.BeginEnemie();
                if (ChangeMaterialParentBeach != null && ChangeMaterialParentBeach.name == "meshenemigoplaya")
                {
                    changeMaterialBeach.BeginEnemieMaterialType2();
                }
                else
                {
                    changeMaterial.BeginEnemieMaterialType();
                }
                break;
        }
    }

    public void enemyHurtFalling()
    {
        if (ParachuteLife2 != null)
        {
            ParachuteLife2.SetActive(true);
        }

        enemiIA = GetComponent<EnemiIA>();
        enemiIA.isStoppedIA = true;
    }
}