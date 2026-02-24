using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public float Health;
    private CameraShake CS;
    public float CurrentHealth;
    public Animator HeadAnim;
    public SpriteRenderer Head;
    public Sprite DieHead;

    private float damageCooldown = 0.1f;
    private float lastDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = Health;
        CS = FindAnyObjectByType<CameraShake>();
        CS.Shake(2f, 4f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            kill();
        } 
        if(CurrentHealth  < Health/3)
        {
            
        }
    }

    public void Damage(float damage)
    {
        if (Time.time < lastDamageTime + damageCooldown)
            return; // cooldowning

        lastDamageTime = Time.time;

        CS.Shake(0.1f, 5f, 5f);
        HeadAnim.SetTrigger("Hit");
        CurrentHealth -= damage;
    }

    void kill()
    {
        PlayerPrefs.SetString("Level", "Level2");

        PlayerPrefs.Save();
        StartCoroutine(FindFirstObjectByType<LevelManager>().transition());
    }
}
