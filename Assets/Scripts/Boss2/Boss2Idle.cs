using UnityEngine;

public class Boss2Idle : StateMachineBehaviour
{
    int A_Value;
    float attackTimer;
    bool attacked;

    public float attackDelay = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = 0f;
        attacked = false;
        FindFirstObjectByType<LaserAtPlayer>().canshoot = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0) || attacked)
            return;

        attackTimer += Time.deltaTime;

        if (attackTimer < attackDelay)
            return;

        A_Value = Random.Range(0, 300);
        attacked = true;

        if (A_Value < 100)
        {
            animator.SetTrigger("Laser");
        }
        else if (A_Value < 200)
        {
            animator.SetTrigger("LaserSky");
        }
        else if (A_Value < 285)
        {
            animator.SetTrigger("SpikeAll");
        }
        else
        {
            FindFirstObjectByType<Boss2>().StartCoroutine(FindFirstObjectByType<Boss2>().GuardDown(10));
            animator.SetTrigger("TPA");
            FindFirstObjectByType<LaserAtPlayer>().canshoot = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Laser");
        animator.ResetTrigger("LaserSky");
        animator.ResetTrigger("SpikeAll");
        animator.ResetTrigger("TPA");
    }
}
