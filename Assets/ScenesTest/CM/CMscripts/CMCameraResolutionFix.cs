using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMCameraResolutionFix : Singleton<CMCameraResolutionFix>
{
    public SpriteRenderer spriteWidth;

    public float ortographSize;

    protected override void Awake()
    {
        //Vector3 myVec = new Vector3(Screen.width, Screen.height, 0f);
        //spriteWidth.bounds.size.x = myVec.x;
        //VERTICAL FIT
        //Camera.main.orthographicSize = spriteWidth.bounds.size.x * Screen.height / Screen.width * 0.5f;
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = spriteWidth.bounds.size.x * Screen.height / Screen.width * 0.5f;

        ortographSize = spriteWidth.bounds.size.x * Screen.height / Screen.width * 0.5f;

        /*
        //HORIZONTAL FIT
        Camera.main.orthographicSize = spriteWidth.bounds.size.y / 2;

        //ENTIRE FIT
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = spriteWidth.bounds.size.x / spriteWidth.bounds.size.y;
        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = spriteWidth.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = spriteWidth.bounds.size.y / 2 * differenceInSize;
        }
        */
    }

    //private void Update()
    //{
    //    //ortographSize = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    //}
}