using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public float Health;
    private CameraShake CS;
    public float CurrentHealth;
    public Animator HeadAnim;
    private float damageCooldown = 0.1f;
    private float lastDamageTime;
    bool guarded = true;

    public float guardDownTime;
    public GameObject Guard, GuardParticle;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = Health;
        CS = FindAnyObjectByType<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            kill();
        }
    }
    public void Damage(float damage)
    {
        if (Time.time < lastDamageTime + damageCooldown)
            return; // still in cooldowning

        lastDamageTime = Time.time;

        if (guarded)
        {
            CS.Shake(0.1f, 4f, 15f);
        }
        else
        {
            CS.Shake(0.1f, 15f, 15f);
            HeadAnim.SetTrigger("Hit");
            CurrentHealth -= damage;
        }
    }
        void kill()
    {
        PlayerPrefs.GetString("Level", "Level3");
        StartCoroutine(FindFirstObjectByType<LevelManager>().transition());
        PlayerPrefs.Save();
    }
    public IEnumerator GuardDown(float time)
    {
        guarded = false;
        Guard.SetActive(false);
        Instantiate(GuardParticle,Guard.transform.position, Quaternion.identity);
        CS.Shake(0.1f, 5f, 5f);
        yield return new WaitForSeconds(time);
        Guard.SetActive(true);
        guarded = true;
    }
}
