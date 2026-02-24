using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAtPlayer : MonoBehaviour
{
    public Transform Player;
    public Animator Anim;

    float angle;
    Vector2 dir;

    public bool canshoot = false;
    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canshoot)
        {
            if (!isShooting)
            {
                StartCoroutine(ShootRand());
            }
        }

    }
    public IEnumerator Shoot()
    {   
        //
        dir = Player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-5f , +5f));
        Anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.1f, 20f, 10f);
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(0.6f);
        //
        dir = Player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-5f, +5f));
        Anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.1f, 20f, 10f);
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(0.6f);
        //
        dir = Player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-5f, +5f));
        Anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.1f, 20f, 10f);
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(0.6f);


    }
    public IEnumerator ShootRand()

    {
        isShooting = true;
        yield return new WaitForSeconds(0.75f);
        dir = Player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-20f, +20f));
        Anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.1f, 20f, 10f);
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(1f);
        isShooting = false;
    }
}
