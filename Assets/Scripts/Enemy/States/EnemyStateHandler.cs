using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [HideInInspector]
    public EnemyManager enemyManager;
    EnemyLocomotion enemyLocomotion;
    EnemyAttacker enemyAttacker;
    EnemyStats enemyStats;
    EnemyAnimatorHandler enemyAnimatorHandler;

    [SerializeField]
    public EnemyState currentState;

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttacker = GetComponent<EnemyAttacker>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();

        currentState = GetComponentInChildren<IdleState>();
    }

    void FixedUpdate()
    {
        HandleCurrentState();
    }

    public void HandleCurrentState(){
        if(currentState != null){
            currentState = currentState.Tick(enemyManager, enemyStats, enemyAnimatorHandler);
        }

        // if(enemyLocomotion.currentTarget != null){
        //     enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyLocomotion.currentTarget.transform.position, transform.position);

        //     enemyLocomotion.HandleMoveToTarget();  
            
        //     if(enemyLocomotion.distanceFromTarget <= enemyLocomotion.stoppingDistance){
        //         //handle attack action
        //         enemyAttacker.AttackTarget();
        //     }
        // }
    }
}
