using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkySummon : MonoBehaviour
{
    private Animator[] LaserOne;
    private Animator[] LaserTwo;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Laser1 = GameObject.FindGameObjectsWithTag("LaserOne");
        GameObject[] Laser2 = GameObject.FindGameObjectsWithTag("LaserTwo");
        LaserOne = System.Array.ConvertAll(Laser1, o => o.GetComponent<Animator>());
        LaserTwo = System.Array.ConvertAll(Laser2, o => o.GetComponent<Animator>());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Summon()
    {
        foreach (Animator anim in LaserOne)
        {
            anim.SetTrigger("Shoot");
        }
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.2f,15f,15f);
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySFX(audioManager.Shake1);
        yield return new WaitForSeconds(0.5f);
        foreach (Animator anim in LaserTwo)
        {
            anim.SetTrigger("Shoot");
        }
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<CameraShake>().Shake(0.2f, 15f, 15f);
        audioManager.PlaySFX(audioManager.Shake1);
    }
}
