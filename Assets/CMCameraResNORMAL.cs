using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CMCameraResNORMAL : MonoBehaviour
{
    public SpriteRenderer spriteWidth;

    public float ortographSize;
    [SerializeField]
    GameObject
    panelBackGround;

    private void Awake()
    {
        Camera.main.orthographicSize = spriteWidth.bounds.size.x * Screen.height / Screen.width * 0.5f;
        StartCoroutine(destroyAnimation());



    }
    
    IEnumerator destroyAnimation()
    {
        yield return new WaitForSeconds(6.25f);
        GameManager.Instance.IsactiveSplash1 = false;
        Destroy(spriteWidth);
        Destroy(panelBackGround);
    }

    //private void Update()
    //{
    //    //ortographSize = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
    //}
}
