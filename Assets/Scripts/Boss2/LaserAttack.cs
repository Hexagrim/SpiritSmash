using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindAnyObjectByType<LaserAtPlayer>().StartCoroutine(FindAnyObjectByType<LaserAtPlayer>().Shoot());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
