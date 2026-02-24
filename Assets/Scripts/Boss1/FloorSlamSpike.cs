using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSlamSpike : StateMachineBehaviour
{
    public GameObject Shockwave;

    private Transform slamPos;

    bool done;

    private CameraShake CS;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CS = GameObject.FindFirstObjectByType<CameraShake>();   
        slamPos = GameObject.FindWithTag("SlamPos").transform;
        done = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1f && !done)
        {
            Instantiate(Shockwave, slamPos.position, Quaternion.identity);
            CS.Shake(0.3f, 5f, 15f);
            AudioManager audioManager = FindFirstObjectByType<AudioManager>();
            audioManager.PlaySFX(audioManager.Shake1);
            animator.SetTrigger("Idle");
            done = true;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this stajjudj
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("FloorSpike");
    }

}
