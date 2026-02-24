using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public CameraShake CS;
    public float shakeTime;
    LevelManager lvlM;
    public Animator Anim1;
    Animator Anim;
    public GameObject Particle;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GameObject.FindWithTag("dt").GetComponent<Animator>();
        dead = false;
        lvlM = FindFirstObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Kill()
    {
        dead = true;
        StartCoroutine(Kell());

    }
    public void Retry()
    {
        StartCoroutine(ret());
    }
    IEnumerator ret()
    {
        Anim1.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    IEnumerator Kell()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Death);
        CS.Shake(shakeTime, 5f, 5f);
        GameObject.FindWithTag("LowhpVig").GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(Particle,FindFirstObjectByType<PlayerMovement>().gameObject.transform.position,Quaternion.identity);
        lvlM.StartCoroutine(lvlM.SmoothSlowMo(1, 0, 0.5f));
        FindFirstObjectByType<HoldToHealCamera>().enabled = false;
        FindFirstObjectByType<PlayerMovement>().enabled = false;
        FindFirstObjectByType<BlobFireMech>().enabled = false;
        FindFirstObjectByType<MouseDash>().enabled = false;
        FindFirstObjectByType<PlayerHealthManager>().enabled = false;
        yield return new WaitForSecondsRealtime(1);
        Anim.SetTrigger("die");

    }
}
