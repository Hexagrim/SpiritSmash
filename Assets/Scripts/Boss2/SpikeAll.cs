using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAll : StateMachineBehaviour
{
    bool done;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        done = false ;
    }
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!done && !animator.IsInTransition(0))
        {
            done = true ;
            FindObjectOfType<SpikeAllSummon>().StartCoroutine(FindObjectOfType<SpikeAllSummon>().Summon());
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("SpikeAll");
    }

    
}
