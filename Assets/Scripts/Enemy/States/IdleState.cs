using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    [Header("Idle state settings")]
    [SerializeField]
    LayerMask detectionLayer;

    [SerializeField]
    float detectionRadius = 10f;
    [SerializeField]
    float maximumDetectionAngle = 50f;
    [SerializeField]
    float minimumDetectionAngle = -50f;

    public override EnemyState Tick(EnemyManager enemyManager){
        if(enemyManager.currentTarget != null && enemyManager.currentPhase.keyState == EnemyPhase.MAIN_STATES.Pursue)
            return GetComponent<PursueState>();
            
        HandleDetection(enemyManager);

        return this;
    }

    public void HandleDetection(EnemyManager enemyManager){
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; ++i){
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            Transform characterTransform = colliders[i].transform.GetComponent<Transform>();

            if(characterStats != null){
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle){
                    enemyManager.currentTarget = characterStats;
                    enemyManager.targetTransform = characterTransform;
                }
            }
        }
    }
}
