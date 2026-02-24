using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BlobFireMech : MonoBehaviour
{
    public GameObject blob;
    private Vector2 target;
    public float speed = 20f;
    public float reSpeed = 50f;
    private float reSpeed_TEMP;
    public float fireDistance = 5f;
    public bool pointReached = true;
    private Rigidbody2D rb;
    public float forceAmount;
    RigidbodyConstraints2D rbc;
    public bool Frozen;
    public float recoilTime;
    public Transform blobPos;

    public SpriteRenderer SR;
    public Collider2D Col;

    public bool check;

    public CameraShake CS;


    public bool playerCanShoot = true;
    public float shootCooldown = 0.5f;

    private void Start()
    {
        reSpeed_TEMP = reSpeed;
        rb = GetComponent<Rigidbody2D>();
        rbc = rb.constraints;
    }
    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pointReached)
        {
            blob.transform.position = blob.transform.position;
        }

        if (Vector2.Distance(blob.transform.position, target) < 0.05f)
        {
            pointReached = true;
        }
        if (Vector2.Distance(blob.transform.position, blobPos.position) < 0.5f)
        {
            if (Input.GetMouseButtonDown(0) && playerCanShoot)
            {
                MoveObj(mouseWorldPos);
                StartCoroutine(ShootCooldownRoutine());
            }
        }
    }

    void MoveObj(Vector2 worldPos)
    {
        pointReached = false;

        Vector2 dir = worldPos - (Vector2)blob.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        blob.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        target = (Vector2)blob.transform.position +
                 (Vector2)blob.transform.right * fireDistance;
        StartCoroutine(ApplyRecoil());
    }
    private void FixedUpdate()
    {
        if (pointReached)
        {
            if (Vector2.Distance(blob.transform.position, blobPos.position) < 0.5f)
            {
                blob.SetActive(false);
                blob.transform.position = blobPos.position;
                SR.enabled = false;
                Col.enabled = false;
                check = false;
                blob.transform.rotation = Quaternion.Euler(0, 0, 0);
                if(blob.transform.parent != this.gameObject)
                {
                    blob.transform.parent = this.gameObject.transform;
                }
            }

            Vector2 dir = blobPos.position - blob.transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            blob.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            blob.transform.position = Vector2.MoveTowards(
                blob.transform.position,
                blobPos.position,
                reSpeed * Time.deltaTime
            );

        }

        else
        {
            blob.transform.parent = null;
            blob.SetActive(true);
            SR.enabled = true;
            Col.enabled = true;
            check = true;

            blob.transform.position = Vector2.MoveTowards(
                blob.transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }

    private IEnumerator ApplyRecoil()
    {
        CS.Shake(0.15f, 4f, 3f);
        Frozen = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(blob.transform.right * new Vector2(-1, -1) * forceAmount);
        yield return new WaitForSeconds(recoilTime);
        Frozen = false;
    }

    private IEnumerator ShootCooldownRoutine()
    {
        playerCanShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        playerCanShoot = true;
    }
}
