using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalChainBehavior : StateMachineBehaviour
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
        rand = Random.Range(0, 101);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            int num = rand;
            Debug.Log(num);
            if (rand <= 50)
            {
                animator.SetTrigger("idle");
            }
            else if (rand > 50)
            {
                animator.SetTrigger("claw");
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
        animator.ResetTrigger("idle");
        animator.ResetTrigger("claw");
    }
}
