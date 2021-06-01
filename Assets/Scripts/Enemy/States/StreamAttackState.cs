using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamAttackState : EnemyState
{
    [SerializeField]
    private GameObject streamPrefab;
    [SerializeField]
    private float streamLifetime = 1f;
    [SerializeField]
    private float cooldownTime = 4f;

    [SerializeField]
    private float cooldownTimer = 4f;
    
    public override EnemyState Tick(EnemyManager enemyManager){
        cooldownTimer -= Time.deltaTime;

        if(streamPrefab != null && cooldownTimer < 0f){
            enemyManager.enemyAnimatorHandler.PlayTargetAnimation("high_cast", true);

            cooldownTimer = cooldownTime;
        }    

        if(!enemyManager.isInteracting && enemyManager.isAttack){
            enemyManager.isInteracting = true;

            Vector3 dir = (enemyManager.targetTransform.position - transform.position).normalized;

            Quaternion streamRotation = Quaternion.LookRotation(dir);
            streamRotation.x = 0;
            streamRotation.z = 0;

            Vector3 streamPos = transform.position;
            streamPos.y = 10.5f;

            GameObject stream = Instantiate(streamPrefab, streamPos, streamRotation);
            Destroy(stream, streamLifetime);
            
            enemyManager.isAttack = false;
        }   

        return GetComponent<IdleState>();
    }
}
