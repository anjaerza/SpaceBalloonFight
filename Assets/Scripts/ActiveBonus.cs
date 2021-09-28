using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveBonus : MonoBehaviour
{
    [SerializeField] GameObject generatorBubble;
    [SerializeField] GameObject popUp;

    private void Awake()
    {
        //popUp.SetActive(false);
    }
    private void OnTriggerExit(Collider other) {
       if(other.tag=="Player")
       {
            popUp.SetActive(true);
            generatorBubble.GetComponent<BubbleGenerator>().enabled=true;
            LvlBonus.Instance.BubbleCountDisapear = 0;
            LvlBonus.Instance.KillCollider1[0].SetActive(true);
            LvlBonus.Instance.KillCollider1[1].SetActive(true);
            LvlBonus.Instance.KillCollider1[2].SetActive(true);
            LvlBonus.Instance.KillCollider1[3].SetActive(true);
        }
       
   }

    



    
}
