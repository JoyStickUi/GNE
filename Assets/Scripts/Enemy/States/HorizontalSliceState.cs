using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSliceState : EnemyState
{
    [SerializeField]
    private GameObject slicePrefab;
    [SerializeField]
    private float sliceLifetime = 5f;
    [SerializeField]
    private float cooldownTime = 10f;

    [SerializeField]
    private float cooldownTimer = 10f;

    bool isComplete = false;
    
    public override EnemyState Tick(EnemyManager enemyManager){
        cooldownTimer -= Time.deltaTime;

        if(slicePrefab != null && cooldownTimer < 0f){
            enemyManager.enemyAnimatorHandler.PlayTargetAnimation("HorizontalSlice", true);

            cooldownTimer = cooldownTime;
        }    

        if(!enemyManager.isInteracting && enemyManager.isAttack){
            Vector3 dir = (enemyManager.targetTransform.position - transform.position).normalized;

            Quaternion sliceRotation = Quaternion.LookRotation(dir);
            sliceRotation.x = 0;
            sliceRotation.z = 0;

            Vector3 slicePos = transform.position;
            slicePos.y = 11f;

            GameObject slice = Instantiate(slicePrefab, slicePos, sliceRotation);
            Destroy(slice, sliceLifetime);
            
            enemyManager.isAttack = false;
            isComplete = true;
        }   

        EnemyState toReturnState = null;
        if(isComplete){
            toReturnState = GetComponent<IdleState>();
        }else{
            toReturnState = this;
        }
        isComplete = false;
        return toReturnState;
    }
}
