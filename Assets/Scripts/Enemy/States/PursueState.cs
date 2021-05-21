using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager){
        if(enemyManager.currentTarget != null){
            enemyManager.enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            if(!enemyManager.isInteracting){
                enemyManager.enemyLocomotion.HandleMoveToTarget();  
            }
            
            //PLACE FOR NEURAL NETWORK ACTIVATION

            if(Random.Range(0f, 10f) > 5f){
                return GetComponent<SwampAttackState>();
            }

            // if(enemyManager.enemyLocomotion.distanceFromTarget <= enemyManager.enemyLocomotion.stoppingDistance){
            //     return GetComponent<StuffAttackState>();
            // }

            return this;
        }
        return GetComponent<IdleState>();
    }
}
