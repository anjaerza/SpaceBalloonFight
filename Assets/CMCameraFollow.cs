using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMCameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void LateUpdate()
    {
        Vector3 newPos = new Vector3();
        newPos.y = player.transform.position.y;
        transform.position = newPos;
    }
}
