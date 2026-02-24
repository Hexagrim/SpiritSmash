using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class LevelThreeManager : MonoBehaviour
{
    public bool canAttack;
    public Transform Player;
    int attackValue;
    CameraShake CS;
    public GameObject Effect;
    public Transform MagicCircPos;
    public GameObject SpikyBall;
    float Interval = 0.3f;
    public Animator Anim;
    public bool second_stage;
    public Animator BossAnim;
    public Animator SpikeAnim;
    // Start is called before the first frame update
    void Start()
    {
        CS = FindAnyObjectByType<CameraShake>();
        StartCoroutine(StageChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (canAttack && !second_stage)
        {
            attackValue = Random.Range(0, 100);
            if (attackValue < 25)
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine(LaserRand());

            }
            else if(attackValue < 50)
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine (LaserHalfL());

            }
            else if(attackValue < 75)
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine(Ball());
            }
            else
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                CS.Shake(2f, 10f, 5f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine(BigLaser(2));
            }
                canAttack = false;
        }
        if(canAttack && second_stage)
        {
            // this stge i wanna do big laser and laseratplayer, cause it suits
            //also cooldown should be 3 secs now
            attackValue = Random.Range(0, 100);
            if(attackValue < 50)
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine(LaserAtPlayer());
            }
            else
            {
                StageEffect();
                CS.Shake(0.2f, 15f, 7f);
                CS.Shake(2f, 10f, 5f);
                AudioManager audioManager = FindFirstObjectByType<AudioManager>();
                audioManager.PlaySFX(audioManager.Shake1);
                StartCoroutine(BigLaser(3f));
            }
            canAttack = false;

        }
    }
    IEnumerator LaserRand()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject[] AnimObj = GameObject.FindGameObjectsWithTag("l3rl");
        foreach (GameObject Laser in AnimObj)
        {
            float angle;
            Vector2 dir;
            dir = Player.position - Laser.transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Laser.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-10f, +10f));
            Laser.GetComponent<Animator>().SetTrigger("Shoot");
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(cooldown(2f));


    }
    IEnumerator cooldown(float t)
    {
        yield return new WaitForSeconds(t);
        canAttack = true;
    }
    IEnumerator LaserHalfL()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject[] AnimObj = GameObject.FindGameObjectsWithTag("left");
        foreach (GameObject Laser in AnimObj)
        {
            Laser.GetComponent<Animator>().SetTrigger("Shoot");
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        GameObject[] AnimObjR = GameObject.FindGameObjectsWithTag("right");
        foreach (GameObject Laser in AnimObjR)
        {
            Laser.GetComponent<Animator>().SetTrigger("Shoot");
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(cooldown(2f));
    }
    IEnumerator BigLaser(float t)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindWithTag("biglazer").GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(cooldown(t));
    }
    void StageEffect()
    {
        Instantiate(Effect, MagicCircPos.position, Quaternion.identity);
    }
    IEnumerator Ball()
    {
        CS.Shake(0.2f, 6f, 4f);
        yield return new WaitForSeconds(0.5f);
        StageEffect();
        GameObject Ball = Instantiate(SpikyBall, MagicCircPos.transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.85f, 1.15f);
        yield return new WaitForSeconds(Interval);
        StageEffect();
        CS.Shake(0.2f, 6f, 4f);
        Ball = Instantiate(SpikyBall, MagicCircPos.transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.85f, 1.15f);
        yield return new WaitForSeconds(Interval);
        StageEffect();
        CS.Shake(0.2f, 6f, 4f);
        Ball = Instantiate(SpikyBall, MagicCircPos.transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.80f, 1.20f);
        yield return new WaitForSeconds(Interval);
        StageEffect();
        CS.Shake(0.2f, 6f, 4f);
        Ball = Instantiate(SpikyBall, MagicCircPos.transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.85f, 1.15f);
        yield return new WaitForSeconds(Interval);
        StageEffect();
        CS.Shake(0.2f, 6f, 4f);
        Ball = Instantiate(SpikyBall, transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.80f, 1.20f);
        yield return new WaitForSeconds(Interval);
        StartCoroutine(cooldown(2f));
    }

    IEnumerator StageChange()
    {
        second_stage = false;
        yield return new WaitForSeconds(20f);
        if (!canAttack) yield return new WaitForSeconds(7f);

        
        second_stage = true;
        BossAnim.SetTrigger("Stage");
        SpikeAnim.SetTrigger("rise");
        
    }

    IEnumerator LaserAtPlayer()
    {
        Vector2 dir = Player.position - Anim.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Anim.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 + Random.Range(-10f, +10f));
        Anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.1f, 20f, 10f);
        StartCoroutine(cooldown(3.5f));
    }

}
