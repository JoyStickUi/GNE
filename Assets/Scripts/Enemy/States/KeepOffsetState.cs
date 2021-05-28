using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOffsetState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager){
        if(enemyManager.currentTarget != null){
            enemyManager.enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            if(!enemyManager.isInteracting){
                enemyManager.enemyLocomotion.HandleKeepOffsetFromTarget();  
            }
            
            //PLACE FOR NEURAL NETWORK ACTIVATION

            if(enemyManager.enemyStats.currentHealth <= 50){
                return GetComponent<PhaseChangingState>();
            }

            return this;
        }
        return GetComponent<IdleState>();
    }
}
