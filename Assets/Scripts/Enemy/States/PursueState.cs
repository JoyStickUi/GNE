using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager){
        if(enemyManager.currentTarget != null){
            enemyManager.enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            enemyManager.enemyLocomotion.HandleMoveToTarget();  
            
            if(enemyManager.enemyLocomotion.distanceFromTarget <= enemyManager.enemyLocomotion.stoppingDistance){
                return GetComponent<StuffAttackState>();
            }

            return this;
        }
        return GetComponent<IdleState>();
    }
}
