using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleFBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    private int rand;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = 0;
        timer = Random.Range(minTime, maxTime);
        rand = Random.Range(0, 301);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            int num = rand;
            Debug.Log(num);
            if (rand <= 100)
            {
                animator.SetTrigger("rAttack");
            }
            else if ((rand > 100) && (rand <= 200))
            {
                animator.SetTrigger("lAttack");
            }
            else if (rand > 200)
            {
                animator.SetTrigger("dAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("rAttack");
        animator.ResetTrigger("lAttack");
        animator.ResetTrigger("dAttack");
    }
}
