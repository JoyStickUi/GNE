using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuingBehaviour : StateMachineBehaviour
{
    EnemyLocomotion enemyLocomotion;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Pursuing");
        enemyLocomotion = animator.gameObject.GetComponentInParent<EnemyLocomotion>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float delta = Time.fixedDeltaTime;
        enemyLocomotion.HandleMovementD(delta);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    } 
}
