using UnityEngine;
using Cinemachine;

public class HoldToHealCamera : MonoBehaviour
{
    public CinemachineVirtualCamera vCam; 
    public KeyCode healKey = KeyCode.E;
    public float holdDuration = 3f;       
    public float zoomSpeed = 10f;        
    public float healZoomSize = 3f;  
    public float normalSize = 5f;     

    private float holdTimer = 0f;
    private bool hasHealed = false;
    public bool isHealing = false;      
    private bool canStartHeal = true;     

    private PlayerMovement movement;
    private MouseDash mousedash;
    private BlobFireMech bfm;
    private PlayerHealthManager healthManager;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        mousedash = GetComponent<MouseDash>();
        bfm = GetComponent<BlobFireMech>();
        healthManager = GetComponent<PlayerHealthManager>();

        if (vCam == null)
            Debug.LogError("Assign a Cinemachine Virtual Camera!");
    }

    void Update()
    {
        if (mousedash.isDashing) return;

        bool canHealNow = healthManager.Soul == 8; 

        if (canStartHeal && Input.GetKey(healKey) && canHealNow)
        {
            isHealing = true;
            canStartHeal = false;
        }

        if (isHealing)
        {
            GameObject.FindWithTag("RecVig").GetComponent<Animator>().SetBool("Rec", true);

            movement.Anim.Play("Idle");
            movement.enabled = false;
            bfm.enabled = false;
            mousedash.enabled = false;

            holdTimer += Time.deltaTime;

            if (holdTimer >= holdDuration && !hasHealed)
            {
                hasHealed = true;
                HealPlayer();
            }
        }
        else
        {
            GameObject.FindWithTag("RecVig").GetComponent<Animator>().SetBool("Rec", false);

            movement.enabled = true;
            bfm.enabled = true;
            mousedash.enabled = true;
        }

        float targetSize = (isHealing && canHealNow && !hasHealed) ? healZoomSize : normalSize;
        vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, targetSize, Time.deltaTime * zoomSpeed);


        if (!Input.GetKey(healKey))
        {
            holdTimer = 0f;
            hasHealed = false;
            isHealing = false;
            canStartHeal = true;

            movement.enabled = true;
            bfm.enabled = true;
            mousedash.enabled = true;
            movement.isFrozen = false;
        }
    }

    void HealPlayer()
    {

        healthManager.Heal(); 
    }
}
