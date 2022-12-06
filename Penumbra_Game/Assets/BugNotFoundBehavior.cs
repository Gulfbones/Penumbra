using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNotFoundBehavior : StateMachineBehaviour
{

    public float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 1;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 1;
        animator.ResetTrigger("idle");
    }


}
