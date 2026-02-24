using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAllSummon : MonoBehaviour
{
    public Animator FloorSpike;
    public GameObject Ball;
    public Transform Head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Summon()
    {
        FloorSpike.SetTrigger("SpikeFloor");
        GameObject ball = Instantiate(Ball , Head.position, Quaternion.identity);
        ball.SetActive(true);
        FindAnyObjectByType<CameraShake>().Shake(0.3f, 10f, 15f);
        yield return new WaitForSeconds(3f);
        FloorSpike.ResetTrigger("SpikeFloor");
    }
}
