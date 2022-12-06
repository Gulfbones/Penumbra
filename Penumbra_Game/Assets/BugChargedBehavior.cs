using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugChargedBehavior : StateMachineBehaviour
{

    public float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 5;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("notFound");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 5;
        animator.ResetTrigger("notFound");
    }


}
