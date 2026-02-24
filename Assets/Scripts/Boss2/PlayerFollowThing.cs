using UnityEngine;
using System.Collections;

public class PlayerFollowThing : MonoBehaviour
{
    public Transform target;
    public Transform followObject;
    public Transform newParent;

    public float followDuration = 2f;
    public float followSpeed = 10f;

    public GameObject Particle;
    private void Start()
    {
        StartCoroutine(FollowRoutine());
    }
    public void StartFollow()
    {
        StartCoroutine(FollowRoutine());
    }

    IEnumerator FollowRoutine()
    {
        float timer = 0f;
        followObject.transform.parent = null;

        while (timer < followDuration)
        {
            // Follow
            followObject.position = Vector3.Lerp(
                followObject.position,
                target.position,
                followSpeed * Time.deltaTime
            );

            // Rotate toward target (normal way)
            Vector2 dir = target.position - followObject.position;
            if (dir.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                followObject.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
                // remove +90f if your sprite already faces right
            }

            timer += Time.deltaTime;
            yield return null;
        }
        Destroy();
    }
    public void Destroy()
    {
        GameObject part = Instantiate(Particle, transform.position, Quaternion.identity);
        GameObject.Destroy(followObject.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy();
        }
    }
}
