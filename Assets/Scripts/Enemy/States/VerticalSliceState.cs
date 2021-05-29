using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSliceState : EnemyState
{
    [SerializeField]
    private GameObject slicePrefab;
    [SerializeField]
    private Transform sliceEmitterPos;
    [SerializeField]
    private float sliceLifetime = 5f;
    [SerializeField]
    private float cooldownTime = 10f;

    [SerializeField]
    private float cooldownTimer = 10f;
    
    public override EnemyState Tick(EnemyManager enemyManager){
        cooldownTimer -= Time.deltaTime;

        if(slicePrefab != null && cooldownTimer < 0f){
            enemyManager.enemyAnimatorHandler.PlayTargetAnimation("VerticalSlice", true);

            cooldownTimer = cooldownTime;
        }    

        if(!enemyManager.isInteracting && enemyManager.isAttack){
            Vector3 dir = (enemyManager.targetTransform.position - transform.position).normalized;

            Quaternion sliceRotation = Quaternion.LookRotation(dir);
            sliceRotation.x = 0;

            Vector3 slicePos = sliceEmitterPos.position;
            slicePos.y = Terrain.activeTerrain.SampleHeight(slicePos);

            GameObject slice = Instantiate(slicePrefab, slicePos, sliceRotation);
            Destroy(slice, sliceLifetime);
            
            enemyManager.isAttack = false;
        }   

        return GetComponent<IdleState>();
    }
}
