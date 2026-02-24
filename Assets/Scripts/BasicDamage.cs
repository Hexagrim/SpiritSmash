using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDamage : MonoBehaviour
{
    PlayerHealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindFirstObjectByType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthManager = FindFirstObjectByType<PlayerHealthManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHurtArea"))
        {
            healthManager.Damage();

        } 
    }

}
