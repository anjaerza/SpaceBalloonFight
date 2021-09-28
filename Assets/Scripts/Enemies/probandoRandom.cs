using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probandoRandom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rx = Random.Range(0f, Screen.width);
                print("rx" + rx);
                float ry = Random.Range(0f, Screen.height);
                print("ry" + ry);

                print($"Object: {gameObject.name}, <{rx},{ry}>");

                Vector3 randomScreenPos = new Vector3(rx, ry, 0f);
                Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);
                print(inWorldPos);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
