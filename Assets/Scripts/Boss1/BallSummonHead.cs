using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallSummonHead : MonoBehaviour
{
    public GameObject SpikyBall;
    public CameraShake CS;
    public float Interval = 0.5f;
    public bool done;
    public Animator Shock;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SummonBall()
    {
        done = false;
        StartCoroutine(Ball());

    }
    public IEnumerator Ball()
    {
        done = false;
        CS.Shake(0.2f, 6f, 4f);
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(Interval);
        Shock.Play("ShockHead");
        GameObject Ball = Instantiate(SpikyBall, transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.85f, 1.15f);
        yield return new WaitForSeconds(Interval);
        Shock.Play("ShockHead");
        CS.Shake(0.2f, 6f, 4f);
        audioManager.PlaySFX(audioManager.Shake1);
        Ball = Instantiate(SpikyBall, transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.85f, 1.15f);
        yield return new WaitForSeconds(Interval);
        Shock.Play("ShockHead");
        CS.Shake(0.2f, 6f, 4f);
        audioManager.PlaySFX(audioManager.Shake1);
        Ball = Instantiate(SpikyBall, transform.position, Quaternion.identity);
        Ball.transform.localScale *= Random.Range(0.80f, 1.20f);
        yield return new WaitForSeconds(Interval);
        done = true;

    }
}
