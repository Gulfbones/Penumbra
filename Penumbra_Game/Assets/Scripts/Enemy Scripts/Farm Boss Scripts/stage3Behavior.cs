using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage3Behavior : StateMachineBehaviour
{
    private int rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0, 4);

        if (rand == 0)
        {
            animator.SetTrigger("idle");
        }
        else if (rand == 1)
        {
            animator.SetTrigger("rAttack");
        }
        else if (rand == 2)
        {
            animator.SetTrigger("lAttack");
        }
        else
        {
            animator.SetTrigger("dAttack");
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
        animator.ResetTrigger("dAttack");

    }
}
