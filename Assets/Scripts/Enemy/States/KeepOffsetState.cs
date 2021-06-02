﻿using System.Collections;
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

            if(enemyManager.enemyStats.currentHealth <= 50){
                return GetComponent<PhaseChangingState>();
            }

            //PLACE FOR NEURAL NETWORK ACTIVATION
            List<float> inputs = new List<float>();
            inputs.Add(Vector3.Distance(enemyManager.targetTransform.position, transform.position));
            inputs.Add(enemyManager.currentTarget.currentHealth);
            inputs.Add(enemyManager.currentTarget.currentStamina);
            float brainOutput = enemyManager.brain.FeedForward(inputs)[0];

            return GetComponent<FireballAttackState>();

            return this;
        }
        return GetComponent<IdleState>();
    }
}