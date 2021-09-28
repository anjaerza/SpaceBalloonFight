using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyFallForce : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    bool isFalling;

    [SerializeField]
    bool isGrounded;

    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float secondsToInflate;

    Rigidbody rb;

    /// <summary>
    /// Direction is provide by collision.
    /// Example:
    /// Collision with the Enemy in Player Script.
    /// private void OnTriggerEnter(Collider other)
    /// {
    ///    if (other.gameObject.tag == "Enemy")
    ///    {
    ///         Vector3 direction = other.gameObject.transform.position - transform.position;
    ///         EnemyFallForce eff = other.gameObject.GetComponent<EnemyFallForce>();
    ///         eff.Direction = direction;
    ///         eff.enabled = true;
    ///     }
    /// }
    /// </summary>
    public Vector3 Direction { get => direction; set => direction = value; }

    private void Start()
    {
        isFalling = false;
        isGrounded = false;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isFalling)
        {
            FallForce();
        }

        if (isGrounded)
        {
            rb.velocity = Vector3.zero;

            //After x seconds isfalling set false and diasable the script again.
            Invoke("InflateBalloon", secondsToInflate);

            isGrounded = false;
        }
    }

    private void FallForce()
    {
        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
        isFalling = true;
    }

    private void InflateBalloon()
    {
        isFalling = false;
        this.enabled = false;
    }
}
