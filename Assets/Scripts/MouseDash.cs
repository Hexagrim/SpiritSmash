using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseDash : MonoBehaviour
{
    public CameraShake CS;
    public float dashSpeed;
    private Rigidbody2D rb;
    public Transform head;
    public bool canDash = true;
    public bool isDashing;
    public float dashTime, cooldown;
    public Transform MainBody;
    float angle;
    public Animator Anim;

    public float kbIncrementAmount = 5;
    public Transform checkPosition;
    public float radius = 0.5f;
    public LayerMask collisionLayers;

    float GS;
    float xScale;
    public float knockBAmount, yMult;

    public Collider2D Col1, Col2;

    public bool Freeze;

    public PlayerMovement PM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            StopCoroutine(AfterDash());
            StopCoroutine(Dash());
            StartCoroutine(Dash());
        }

        if (isDashing)
            head.rotation = Quaternion.Euler(0f, 0f, angle);
        else
            head.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (checkPosition == null) return;

        Collider2D hit = Physics2D.OverlapCircle(checkPosition.position, radius, collisionLayers);

        if (hit != null && isDashing && !Freeze)
        {
            GetComponent<PlayerHealthManager>().invinsible = true;
            StopCoroutine(AfterDash());
            StopCoroutine(Dash());
            StartCoroutine(AfterDash());
            GetComponent<PlayerHealthManager>().invinsible = true;

            if (hit.gameObject.CompareTag("EnemyOBJ"))
            {
                GetComponent<PlayerHealthManager>().IncreaseSoul();
            }
            else if (hit.gameObject.CompareTag("HurtBall"))
            {

                hit.GetComponent<TransformBallCode>().Destroy();
            }
            else if (hit.gameObject.CompareTag("BossHead"))
            {
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake);
                FindAnyObjectByType<BossOne>().Damage(1);
                GetComponent<PlayerHealthManager>().IncreaseSoul();

            }
            else if (hit.gameObject.CompareTag("GuardBoss2"))
            {

                FindAnyObjectByType<Boss2>().StartCoroutine(FindAnyObjectByType<Boss2>().GuardDown(3));
            }
            else if (hit.gameObject.CompareTag("HeadBoss2"))
            {
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake);
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                GetComponent<PlayerHealthManager>().IncreaseSoul();
                FindAnyObjectByType<Boss2>().Damage(1);
            }

        }

        if (PM.isGrounded && !isDashing)
            canDash = true;
    }

    IEnumerator Dash()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Dash);
        GetComponent<PlayerHealthManager>().invinsible = true;
        Col1.enabled = false;
        Col2.enabled = false;
        CS.Shake(0.3f, 5f, 3f);
        isDashing = true;
        canDash = false;
        GS = rb.gravityScale;
        rb.gravityScale = 0f;
        xScale = MainBody.transform.localScale.x;
        Anim.SetBool("isDashing", true);
        Anim.SetTrigger("Dash");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = mousePos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        MainBody.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        Vector2 directionV = (mousePos - transform.position).normalized;
        rb.velocity = directionV * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        Anim.SetBool("isDashing", false);
        MainBody.transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        isDashing = false;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);

        rb.gravityScale = GS;
        Col1.enabled = true;
        Col2.enabled = true;
        GetComponent<PlayerHealthManager>().invinsible = false;
    }

    IEnumerator AfterDash()
    {
        GetComponent<PlayerHealthManager>().invinsible = true;
        CS.Shake(0.1f, 3f, 2f);
        Freeze = true;
        
        Vector2 fDir = rb.velocity.normalized;
        //from this

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // this 
        mousePos.z = 0f;
        Vector2 directionV = (mousePos - transform.position).normalized;

        //to this


        //if (Mathf.Abs(fDir.x) > 0.25)
        //    rb.velocity = new Vector2(Mathf.Sign(fDir.x) * knockBAmount, knockBAmount * yMult);
        //else
        //    rb.velocity = new Vector2(Mathf.Sign(fDir.x) * knockBAmount * 0.5f, knockBAmount * yMult);

        Anim.SetBool("isDashing", false);
        MainBody.transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        isDashing = false;
        rb.gravityScale = GS;

        yield return new WaitForSeconds(0.2f);
        Freeze = false;
        Col1.enabled = true;//remove these two if error
        Col2.enabled = true;

        yield return new WaitForSeconds(1f);
        GetComponent<PlayerHealthManager>().invinsible = false;
    }
}
