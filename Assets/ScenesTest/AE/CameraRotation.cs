using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    [SerializeField] float angle;
    [SerializeField] GameObject player;

    float rotation;

    void Update()
    {
        if (player != null)
        {
            rotation = angle * player.GetComponent<ScreenWrapper>().RelativePos;
            transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);
        }
    }
}
