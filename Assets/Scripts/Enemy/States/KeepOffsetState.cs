using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class KeepOffsetState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager){
        if(enemyManager.currentTarget != null){
            enemyManager.enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            if(!enemyManager.isAttack){
                enemyManager.enemyLocomotion.HandleKeepOffsetFromTarget();  
            }

            if(enemyManager.enemyStats.currentHealth <= 50){
                return GetComponent<PhaseChangingState>();
            }

            //PLACE FOR NEURAL NETWORK ACTIVATION
            List<float> inputs = new List<float>();
            inputs.Add(Vector3.Distance(enemyManager.targetTransform.position, transform.position));
            inputs.Add(enemyManager.currentTarget.currentHealth);
            inputs.Add(enemyManager.currentTarget.currentStamina);
            inputs.Add(enemyManager.currentTarget.playerManager.playerLocomotion.movementSpeedModifier);
            List<float> networkOutput = enemyManager.brain.FeedForward(inputs);
            int attackIndex = networkOutput.FindIndex(v => networkOutput.Max() == v);

            switch(attackIndex){
                case 0:
                    return GetComponent<FireballAttackState>();
                case 1:
                    return GetComponent<SwampAttackState>();
                case 2:
                    return GetComponent<StreamAttackState>();
            }

            return this;
        }
        return GetComponent<IdleState>();
    }
}
