using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public Animator anim, Hanim;

    private float lastSoulTime = -Mathf.Infinity; //
    public float soulCooldown = 0.1f;

    //hackatime streak management goes hard af
    public bool invinsible;
    public int PlayerHealth = 4;

    public GameObject healthBar1, healthBar2, healthBar3, healthBar4;

    public int CurrentHealth;

    public DeathManager DeathMgr;

    public bool F, Th, Tw, O;

    public CameraShake CS;

    public float InvincibleTime;

    public int Soul;

    public Animator H_Anim;
    public bool GOD_MODE;
    int previousSoul;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = PlayerHealth;
        invinsible = false;
        previousSoul = 1;

    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentHealth == 4 && !F)
        {
            GameObject.FindWithTag("LowhpVig").GetComponent<Animator>().SetBool("LowHP", false);
            CS.Shake(0.5f, 3f, 5f);
            healthBar1.SetActive(true);
            healthBar2.SetActive(true);
            healthBar3.SetActive(true);
            healthBar4.SetActive(true);
            F = true;
            Th = false;
            Tw = false;
            O = false;
        }
        else if (CurrentHealth == 3 && !Th)
        {
            GameObject.FindWithTag("LowhpVig").GetComponent<Animator>().SetBool("LowHP", false);
            CS.Shake(0.5f, 3f, 5f);
            healthBar1.SetActive(false);
            healthBar2.SetActive(true);
            healthBar3.SetActive(true);
            healthBar4.SetActive(true);
            Th = true;
            Tw = false;
            O = false;
            F = false;
        }
        else if (CurrentHealth == 2 && !Tw)
        {
            GameObject.FindWithTag("LowhpVig").GetComponent<Animator>().SetBool("LowHP", false);
            CS.Shake(0.5f, 3f, 5f);
            healthBar1.SetActive(false);
            healthBar2.SetActive(false);
            healthBar3.SetActive(true);
            healthBar4.SetActive(true);
            Tw = true;
            Th = false;
            O = false;
            F = false;
        }
        else if (CurrentHealth == 1 && !O)
        {
            GameObject.FindWithTag("LowhpVig").GetComponent<Animator>().SetBool("LowHP", true);
            CS.Shake(0.5f, 3f, 5f);
            healthBar1.SetActive(false);
            healthBar2.SetActive(false);
            healthBar3.SetActive(false);
            healthBar4.SetActive(true);
            O = true;
            Th = false;
            Tw = false;
            F = false;
        }
        else if (CurrentHealth == 0)
        {

            CS.Shake(0.5f, 3f, 5f);
            healthBar1.SetActive(false);
            healthBar2.SetActive(false);
            healthBar3.SetActive(false);
            healthBar4.SetActive(false);
            O = false;
            Th = false;
            Tw = false;
            F = false;
            DeathMgr.Kill();
            GameObject.FindWithTag("body").SetActive(false);

        }


        if (Soul != previousSoul)
        {
            string triggerName = "";

            if (Soul == 0) triggerName = "empty";
            else if (Soul == 1) triggerName = "one";
            else if (Soul == 2) triggerName = "two";
            else if (Soul == 3) triggerName = "three";
            else if (Soul == 4) triggerName = "four";
            else if (Soul == 5) triggerName = "five";
            else if (Soul == 6) triggerName = "six";
            else if (Soul == 7) triggerName = "seven";
            else if (Soul == 8) triggerName = "full";

            H_Anim.SetTrigger(triggerName);

            // Update previous Soul
            previousSoul = Soul;
        }
        if (GOD_MODE)
        {
            CurrentHealth = PlayerHealth;
        }
    }
    public void Damage()
    {
        if (!invinsible)
        {
            Hanim.SetTrigger("Hurt");
            anim.SetTrigger("Hurt");
            StartCoroutine(TimeStop(0.1f));
            CS.Shake(0.1f, 10f, 10f);
            CurrentHealth -= 1;
            StartCoroutine(InvincibleCounter());
        }

    }

    IEnumerator InvincibleCounter()
    {
        invinsible = true;
        yield return new WaitForSeconds(InvincibleTime);
        invinsible = false;
    }
    IEnumerator TimeStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }


    public void IncreaseSoul()
    {
        if (Time.time - lastSoulTime < soulCooldown)
            return;

        if (Soul != 8)
        {
            Soul += 1;
        }

        lastSoulTime = Time.time;
    }

    public void Heal()
    {
        //heal if soul is fur, now eight
        if (Soul == 8){
            Soul = 0;
            if (CurrentHealth != 4)
            {
                CurrentHealth += 1;
            }
        }
    }
}
