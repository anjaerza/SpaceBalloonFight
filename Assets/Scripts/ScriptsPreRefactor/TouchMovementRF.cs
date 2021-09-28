using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TouchMovementRF : MonoBehaviour
{
    [SerializeField] float forceY, forceYTwoLives, forceYOneLive;
    [SerializeField] float forceX;
    [SerializeField] float dragY, dragTwoLives, dragOneLive;

    float width;
    float height;

    Rigidbody rb;
    PlayerLife2 pl;

    private void Awake()
    {
        width = Screen.width;
        height = Screen.height;

        rb = GetComponent<Rigidbody>();
        pl = GetComponentInChildren<PlayerLife2>();
    }

    private void Start()
    {
        ChangeForceDrag();
    }

    public void ChangeForceDrag()
    {
        //print("Force & Drag was changed!");

        int lives = pl.Lifes;

        if (lives == 2)
        {
            forceY = forceYTwoLives;
            dragY = dragTwoLives;
        }
        if (lives == 1)
        {
            forceY = forceYOneLive;
            dragY = dragOneLive;
        }

        rb.drag = dragY;
    }

    private void FixedUpdate()
    {
        #region Movement Build V0.12

        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);

            //Firs action when touch screen
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

                Vector2 myNewCenterPoint = new Vector2(Screen.width / 2, 0);
                Vector3 myNewCenterPointWorld = Camera.main.ScreenToWorldPoint(myNewCenterPoint);
                myNewCenterPointWorld.z = transform.position.z;

                Vector2 direction = touchWorldPos - myNewCenterPointWorld;

                if (direction.y < 0)
                {
                    direction.y *= -1;
                }

                JumpDirection(direction.normalized);
            }

            //if touch stay on screen or touch moving arround screen without release
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                //Left
                if (touch.position.x < width / 2)
                {
                    MovementX(Vector3.left);
                }
                //Right
                if (touch.position.x > width / 2)
                {
                    MovementX(Vector3.right);
                }
            }
        }

        #endregion
    }

    private void MovementX(Vector3 direction)
    {
        rb.AddForce(direction * forceX);
    }

    private void JumpDirection(Vector3 direction)
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        velocity.x = 0f;
        rb.velocity = velocity;

        rb.AddForce(direction * forceY, ForceMode.Impulse);
    }

    public void JumpY()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        velocity.x = 0f;
        rb.velocity = velocity;

        rb.AddForce(Vector3.up * forceY, ForceMode.Impulse);
    }
}
