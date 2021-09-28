using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 oldPosition;
    float lerpTime=0.005f;
    [SerializeField]float longTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position;
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, lerpTime);
        if (CheckIfTeleport() == true)
        {
            lerpTime = longTime;
            Debug.Log("Telerported");

        }
        else
        {
            lerpTime = 0.05f;
        }
    }

    bool CheckIfTeleport()
    {
        bool teleported=false;
        if (transform.position.x - oldPosition.x >= 10f)
        {
            teleported = true;
        }

        else
        {
            teleported = false;
        }

        return teleported;
    }
}
