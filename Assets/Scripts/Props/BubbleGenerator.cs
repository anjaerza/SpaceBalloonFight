using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField]
    float fireRate, startTime;

    [SerializeField]
    GameObject[] tubes;

    List<Vector3> tubesPositions;

    int lastTube;

    private int bubbleCounter = 0;
    private bool endBubbles = false;

    private void Awake()
    {
        tubesPositions = new List<Vector3>();
        lastTube = 0;
    }

    
    private void Start()
    {
        Invoke("InitializeTubes", startTime); //INVOKE IS NECESRY CUZ OBJECT POOL INSTANTIATE IN START
        //InitializeTubes();
    }

    private void InitializeTubes()
    {
        foreach (GameObject tube in tubes)
        {
            tubesPositions.Add(tube.transform.position);
        }

        //PrintTest
        //for (int i = 0; i < tubesPositions.Count; i++)
        //{
        //    print(tubesPositions[i]);
        //}

        StartCoroutine(LunchBubble(fireRate));
    }

    private IEnumerator LunchBubble(float fireRate)
    {
        //Random tube
        int nextTube = UnityEngine.Random.Range(0, tubesPositions.Count);

        if (lastTube == nextTube)
        {
            //change tube
            StartCoroutine(LunchBubble(fireRate));
            yield break;
        }
        else
        {
            //Normal Instantiate
            //GameObject newBubble = Instantiate(bubble, transform.position, Quaternion.identity, transform);

            //Pool "Instantiate"
            GameObject myBubble = ObjectPool.Instance.GetPooledObject();
            if (myBubble != null && bubbleCounter <= 8)
            {
                bubbleCounter += 1;

                
                //Get tube position
                Vector3 tubePosition = tubesPositions[nextTube];

                //Change bubble position
                myBubble.transform.position = tubePosition;
                myBubble.SetActive(true);
                
                if(bubbleCounter == 8 )
                {
                   // StartCoroutine(TimeToActivePanel(0.2f));                   
                    
                }
            }
            else
            {
                // endBubbles = true;
                yield break;
            }
        }

        lastTube = nextTube;

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(LunchBubble(fireRate));
    }

    private IEnumerator TimeToActivePanel(float Time){
        yield return  new  WaitForSeconds(Time);
        LvlBonus.Instance.ActivePanel();
    }
}
