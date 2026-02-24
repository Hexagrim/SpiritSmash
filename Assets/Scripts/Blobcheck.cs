using UnityEngine;

public class Blobcheck : MonoBehaviour
{
    public BlobFireMech bs;
    public float hitRadius = 0.2f;        // radius to check around blob
    public LayerMask hitMask;             // which layers count as "hit"
    public CameraShake CS;
    private MouseDash dash;
    private PlayerHealthManager healthManager;
    private void Start()
    {
        dash = FindFirstObjectByType<MouseDash>();
        healthManager = FindFirstObjectByType<PlayerHealthManager>();
    }
    void FixedUpdate()
    {
        // Perform overlap check at the blob's current position
        Collider2D hit = Physics2D.OverlapCircle(bs.blob.transform.position, hitRadius, hitMask);
        if (FindAnyObjectByType<HoldToHealCamera>().isHealing)
        {
            return;
        }
        if (hit != null && bs.check)
        {
            bs.pointReached = true;
            CS.Shake(0.15f, 4f, 3f);
            Debug.Log("Hit! -> " + hit.name);
            if (hit.gameObject.CompareTag("EnemyOBJ"))
            {
                dash.canDash = true;
                healthManager.IncreaseSoul();
            }
            else if (hit.gameObject.CompareTag("HurtBall"))
            {
                dash.canDash = true;
                hit.gameObject.GetComponent<TransformBallCode>().Destroy();
            }
            else if (hit.gameObject.CompareTag("BossHead"))
            {
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake);
                FindAnyObjectByType<BossOne>().Damage(1);
                dash.canDash = true;
                healthManager.IncreaseSoul();
            }
            else if (hit.gameObject.CompareTag("GuardBoss2"))
            {
                dash.canDash = true;
                FindAnyObjectByType<Boss2>().StartCoroutine(FindAnyObjectByType<Boss2>().GuardDown(2));
            }
            else if (hit.gameObject.CompareTag("HeadBoss2"))
            {
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake);
                dash.canDash = true;
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                healthManager.IncreaseSoul();
                FindAnyObjectByType<Boss2>().Damage(1);
            }
            else if (hit.gameObject.CompareTag("Boss2Ball"))
            {
                dash.canDash = true;
                hit.gameObject.GetComponent<PlayerFollowThing>().Destroy();
                healthManager.IncreaseSoul();
            }
            else if (hit.gameObject.CompareTag("Hitball"))
            {
                dash.canDash = true;
            }
        }
    }

    // Optional: visualize overlap area in editor
    private void OnDrawGizmos()
    {
        if (bs != null && bs.blob != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(bs.blob.transform.position, hitRadius);
        }
    }
}
