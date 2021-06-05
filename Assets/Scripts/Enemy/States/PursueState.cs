using System.Collections;
using System.Linq;
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

            if(enemyManager.enemyStats.isDead){
                return GetComponent<DeathState>();
            }

            List<float> inputs = new List<float>();
            inputs.Add(Vector3.Distance(enemyManager.targetTransform.position, transform.position));
            List<float> networkOutput = enemyManager.brain.FeedForward(inputs);
            int attackIndex = networkOutput.FindIndex(v => networkOutput.Max() == v);

            switch(attackIndex){
                case 0:
                    return GetComponent<HorizontalSliceState>();
                case 1:
                    return GetComponent<VerticalSliceState>();
            }
            
            return this;
        }
        return GetComponent<IdleState>();
    }
}
