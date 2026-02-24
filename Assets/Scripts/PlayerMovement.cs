using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator Anim;
    private Rigidbody2D rb;
    public float moveSpeed;
    public float moveSpeedAir;
    public float jumpAmount;
    public Transform groundCheckPoint;  
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded;
    float dir;
    public float velPower;

    public float decceleration;
    public float acceleration;
    //coyote time and jump buffer

    float coyoteTime = 0.15f;
    float coyoteTimeCounter;

    float jumpBufferTime = 0.15f;
    float jumpBufferCounter;

    private BlobFireMech BF_Mech;

    float targetSpeed;
    public bool isFrozen = false;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        BF_Mech = GetComponent<BlobFireMech>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MouseDash>().isDashing || GetComponent<MouseDash>().Freeze )

        {
            
            coyoteTimeCounter = 0f;
            return;
        }

        if (dir == 0)
        {
            Anim.SetBool("isRunning", false);
        }
        else
        {
            Anim.SetBool("isRunning", true);
        }
        if (BF_Mech.Frozen)
        {
            coyoteTimeCounter = 0f;
            return;
           
        }
        isGrounded = Physics2D.OverlapCircle(
        groundCheckPoint.position,
        groundCheckRadius,
        groundLayer
        ); 


        if (isGrounded)
        {
            Anim.SetBool("isJumping", false);
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            Anim.SetBool("isJumping", true);
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -=Time.deltaTime;
        }
        if (jumpBufferCounter>0f && coyoteTimeCounter > 0f)
        {
            Jump();

            jumpBufferCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
 
            coyoteTimeCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
        }
        //    if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        //{
        //    rb.velocity = new Vector2(0f, rb.velocity.y);
        //}

        //if (isGrounded)
        //{
        //    rb.drag = 3;
        //}
        //else
        //{
        //    rb.drag = 0;
        //}

        if (rb.velocity.y <= 0)
        {
            rb.gravityScale = 6f;
        }
        else
        {
            rb.gravityScale = 2f;
        }


        if (Input.GetKey(KeyCode.D) && dir != 1)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = 1;
        }

        if (Input.GetKey(KeyCode.A) && dir != -1)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = -1;
        }



        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {

            dir = 0;
        }

    }

    private void FixedUpdate()
    {
        if (GetComponent<MouseDash>().isDashing || GetComponent<MouseDash>().Freeze)
        {
            return;
        }
        if (BF_Mech.Frozen)
        {
            return;
        }
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        //}


        //new movement



        if (isGrounded)
        {
            targetSpeed = moveSpeed * dir;
        }
        else
        {
            targetSpeed = moveSpeedAir * dir;
        }
        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate,velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }


    void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;
        Gizmos.color = isGrounded ? Color.red : Color.green;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);

    }
    void Jump()
    {
        Anim.SetTrigger("takeoff");
        rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
    }
}
