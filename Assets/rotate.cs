using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField]string type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (type =="red")
        {
            transform.Rotate(transform.right * rotationSpeed);
        }
        if (type == "green")
        {
            transform.Rotate(transform.up * rotationSpeed);
        }
        else
        {
            transform.Rotate(transform.forward * rotationSpeed);
        }
       
    }
}
