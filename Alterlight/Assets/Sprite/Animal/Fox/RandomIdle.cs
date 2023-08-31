using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdle : StateMachineBehaviour
{
    [SerializeField]
    private float timeIdle;

    [SerializeField]
    private int numberOfAnimations;

    private bool isIdle;
    private float idleTime;
    private int Animation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isIdle == false)
        {
            idleTime += Time.deltaTime;

            if (idleTime > timeIdle && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isIdle = true;
                Animation = Random.Range(1, numberOfAnimations + 1);
                Animation = Animation * 2 - 1;

                animator.SetFloat("idleValue", Animation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("idleValue", Animation, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (isIdle)
        {
            Animation--;
        }

        isIdle = false;
        idleTime = 0;
    }
}
