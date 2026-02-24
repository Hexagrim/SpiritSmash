using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportBoss1 : StateMachineBehaviour
{
    private Transform Player;
    bool teleported;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        teleported = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.IsInTransition(0) && !teleported)
        {
            animator.gameObject.transform.position = new Vector2(Random.Range(-13, 33), animator.gameObject.transform.position.y);
            animator.SetTrigger("Idle");
            teleported = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Teleport");
    }

    
}
