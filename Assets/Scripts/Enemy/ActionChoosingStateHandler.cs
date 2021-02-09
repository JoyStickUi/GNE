// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ActionChoosingStateHandler : StateMachineBehaviour
// {
//     Transform playerTransform;
//     Transform enemyTransform;
//     EnemyManager enemyManager;

//     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         enemyManager = animator.gameObject.GetComponentInParent<EnemyManager>();
//         playerTransform = animator.gameObject.GetComponent<BehaviourPubVariables>().playerTransform;
//         enemyTransform = animator.gameObject.transform;
//     }

//     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);
//         // if(distanceToPlayer < enemyManager.maxDistance && distanceToPlayer > enemyManager.minDistance){
//         //     Debug.Log(distanceToPlayer);
//         //     animator.CrossFade("Pursuing", 0.0f);
//         // }
//     }

//     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
       
//     }
// }
