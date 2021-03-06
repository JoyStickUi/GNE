﻿using System.Collections;
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

    public float cooldownTimer = 4f;

    bool isComplete = false;

    public void TimerTick(){
        cooldownTimer -= Time.deltaTime;
    }
    
    public override EnemyState Tick(EnemyManager enemyManager){

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
            isComplete = true;
        }   

        EnemyState toReturnState = null;
        if(isComplete || cooldownTimer > 0f){
            toReturnState = GetComponent<IdleState>();
        }else{
            toReturnState = this;
        }
        isComplete = false;
        return toReturnState;
    }
}
