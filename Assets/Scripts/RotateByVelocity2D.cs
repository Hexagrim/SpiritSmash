using UnityEngine;
using System.Collections;
using System;

public class RotateHeadByVelocity2D : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;       // Main rigidbody (body)
    public Transform head;       // Head transform (rotates only)

    [Header("Rotation Settings")]
    public float rotationLerpSpeed = 10f;
    public float angleOffset = 0f;   // Adjust if sprite faces wrong direction
    public float minSpeed = 0.2f;   // Prevent jitter when almost stopped

    //woah hey wtf are you doing here
    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (rb == null || head == null || GetComponent<PlayerMovement>().isGrounded)
        {
            head.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        Vector2 velocity = rb.velocity;

        if (velocity.magnitude < minSpeed)
            return;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle += angleOffset;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        head.rotation = Quaternion.Lerp(
            head.rotation,
            targetRotation,
            rotationLerpSpeed * Time.fixedDeltaTime

        );


    }// woah hey what the fuck is going on ting ding ding

    
}
