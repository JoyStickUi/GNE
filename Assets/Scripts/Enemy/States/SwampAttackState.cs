using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampAttackState : EnemyState
{
    [SerializeField]
    private float minSwampSpawnDistance = 2f;
    [SerializeField]
    private float maxSwampSpawnDistance = 5f;
    [SerializeField]
    private GameObject swampPrefab;
    [SerializeField]
    private float swampLifetime = 9f;
    [SerializeField]
    private float cooldownTime = 10f;

    public float cooldownTimer = 10f;
    
    bool isComplete = false;

    public void TimerTick(){
        cooldownTimer -= Time.deltaTime;
    }

    public override EnemyState Tick(EnemyManager enemyManager){

        if(swampPrefab != null && cooldownTimer < 0f){
            //swamp attack animation
            enemyManager.enemyAnimatorHandler.PlayTargetAnimation("high_cast", true);

            cooldownTimer = cooldownTime;
        }    

        if(!enemyManager.isInteracting && enemyManager.isAttack){
            //swamp object spawn
            Vector3 swampPos = new Vector3(
                enemyManager.targetTransform.position.x + (Random.Range(minSwampSpawnDistance, maxSwampSpawnDistance) * Random.Range(-1f, 1f)),
                enemyManager.targetTransform.position.y,
                enemyManager.targetTransform.position.z + (Random.Range(minSwampSpawnDistance, maxSwampSpawnDistance) * Random.Range(-1f, 1f))
            );

            swampPos.y = Terrain.activeTerrain.SampleHeight(swampPos) + 0.1f;
            
            GameObject swamp = Instantiate(swampPrefab, swampPos, Quaternion.identity);
            swamp.GetComponent<Swamp>().swampLifetime = swampLifetime;
            
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
