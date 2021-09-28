using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePosRB : MonoBehaviour
{
    float height, width;

    [SerializeField]
    float speed, force;

    Rigidbody rb;

    private void Awake()
    {
        height = Screen.height;
        width = Screen.width;

        rb = GetComponent<Rigidbody>();

        Debug.Log("Width: " + width);
        Debug.Log("Height: " + height);
    }

    private void FixedUpdate()
    {
        //force = CMDebugger.Instance.sliderForce.value;
        //speed = CMDebugger.Instance.sliderSpeed.value;

        //rb.drag = CMDebugger.Instance.sliderDrag.value;
        //rb.angularDrag = CMDebugger.Instance.sliderAng.value;

        //CMDebugger.Instance.debugText1.text = "Drag: " + rb.drag;
        //CMDebugger.Instance.debugText2.text = "Ang: " + rb.angularDrag;
        //CMDebugger.Instance.debugText3.text = "Force: " + force;
        //CMDebugger.Instance.debugText5.text = "Speed: " + speed;

        Touch[] myTouches = Input.touches;

        foreach (Touch touch in myTouches)
        {
            //Primer tap ejecuta el jump
            if (touch.phase == TouchPhase.Began)
            {
                Jump();
            }

            //Izquierda
            if (touch.position.x < width / 2)
            {
                Vector3 movement = Vector3.left * speed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }

            //Derecha
            else if (touch.position.x > width / 2)
            {
                Vector3 movement = Vector3.right * speed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }
        }

        //CMDebugger.Instance.debugText4.text = "TouchCount: " + Input.touchCount.ToString();
    }

    public void Jump()
    {
        //Debug.Log("Jump!");
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        rb.velocity = velocity;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
