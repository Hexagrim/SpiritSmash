using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleBoss : StateMachineBehaviour
{
    int Avalue;
    float attackTimer = 0f;        // Counts time since last attack
    public float attackCooldown = 2f; // How often the boss attacks
    private Transform Player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = 0f;
        // Do NOT reset attackTimer here, let it persist while in this state
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer += Time.deltaTime; // count up each frame

        if (attackTimer < attackCooldown)
            return; // still waiting

        // Timer done  trigger attack
        Avalue = Random.Range(0, 100);

        if (Mathf.Abs(Player.position.x - animator.transform.position.x) < 4f &&
            Random.Range(0, 3) == 1)
        {
            animator.SetTrigger("FloorSpike");
        }
        else if (Mathf.Abs(Player.position.x - animator.transform.position.x) < 7f &&
                 Random.Range(0, 3) == 1)
        {
            animator.SetTrigger("Teleport");
        }
        else
        {
            if (Avalue < 35)                 // 40%
                animator.SetTrigger("Ball");
            else if (Avalue < 60)            // 20%
                animator.SetTrigger("AirSpike");
            else if (Avalue < 90)            // 20%
                animator.SetTrigger("FloorSpike");
            else                             // 20%
                animator.SetTrigger("Teleport");
        }

        attackTimer = 0f; // reset timer
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("AirSpike");
        //animator.ResetTrigger("FloorSpike");
        //animator.ResetTrigger("Ball");
        //animator.ResetTrigger("Teleport");

    }
}
