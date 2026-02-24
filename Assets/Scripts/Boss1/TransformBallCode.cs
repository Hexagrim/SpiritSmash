using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TransformBallCode : MonoBehaviour
{
    private CameraShake CS;
    private Transform target;        
    public float maxSpeed = 10f;    
    public float minSpeed = 2f;    
    public float curveStrength = 2f; 
    public float straightenSpeed = 1f; 

    private Vector3 targetPos;
    private Vector3 startPos;
    private Vector3 curveDir;       
    private float noiseOffset;
    private Vector3 moveDir;     
    private bool reachedTarget = false; 

    public GameObject Particle;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        CS = FindFirstObjectByType<CameraShake>();
        startPos = transform.position;
        targetPos = target.position;  // Only grab position once
        noiseOffset = Random.Range(0f, 100f);

        // Random perpendicular direction for curve (left/right)
        Vector3 toTarget = (targetPos - startPos).normalized;
        curveDir = new Vector3(-toTarget.y, toTarget.x, 0).normalized;
        if (Random.value > 0.5f) curveDir *= -1;

        // Start movement toward target
        moveDir = (targetPos - startPos).normalized;
        Invoke("Destroy", 20);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPos);

        // Check if target reached
        if (!reachedTarget && distance < 0.1f)
        {
            reachedTarget = true;
            moveDir = (targetPos - startPos).normalized; 
        }

        if (curveStrength > 0f && !reachedTarget)
        {

            Vector3 dir = (targetPos - transform.position).normalized;


            float noise = Mathf.PerlinNoise(Time.time * 2f, noiseOffset) - 0.5f;

            moveDir = dir + curveDir * noise * curveStrength;
            moveDir.Normalize();
            curveStrength = Mathf.MoveTowards(curveStrength, 0f, straightenSpeed * Time.deltaTime);
        }

        float speed = Mathf.Lerp(minSpeed, maxSpeed, 1f - (curveStrength / 2f));

        transform.position += moveDir * speed * Time.deltaTime;
    }

    public void Destroy()
    {
        CS.Shake(0.1f, 6f, 2f);
        Instantiate(Particle, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
