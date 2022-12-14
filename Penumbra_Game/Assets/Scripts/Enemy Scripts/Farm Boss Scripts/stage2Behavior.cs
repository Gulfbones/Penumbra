using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2Behavior : StateMachineBehaviour
{
    private int rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0, 3);

        if (rand == 0)
        {
            animator.SetTrigger("idle");
        }
        else if (rand == 1)
        {
            animator.SetTrigger("rAttack");
        }
        else
        {
            animator.SetTrigger("lAttack");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("idle");
        animator.ResetTrigger("rAttack");
        animator.ResetTrigger("lAttack");
    }
}
