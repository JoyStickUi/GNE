using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler){
        if(enemyManager.currentTarget != null){
            enemyManager.enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            enemyManager.enemyLocomotion.HandleMoveToTarget();  
            
            if(enemyManager.enemyLocomotion.distanceFromTarget <= enemyManager.enemyLocomotion.stoppingDistance){
                //handle attack state switch
            }

            return this;
        }
        return GetComponent<IdleState>();
    }
}
