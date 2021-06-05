using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttackState : EnemyState
{
    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private Transform fireballEmitterPos;
    [SerializeField]
    private float fireballLifetime = 9f;
    [SerializeField]
    private float cooldownTime = 10f;

    [SerializeField]
    private float cooldownTimer = 10f;
    
    bool isComplete = false;
    
    public override EnemyState Tick(EnemyManager enemyManager){
        cooldownTimer -= Time.deltaTime;

        if(fireballPrefab != null && cooldownTimer < 0f){
            enemyManager.enemyAnimatorHandler.PlayTargetAnimation("light_cast", true);

            cooldownTimer = cooldownTime;
        }    

        if(!enemyManager.isInteracting && enemyManager.isAttack){
            GameObject fireball = Instantiate(fireballPrefab, fireballEmitterPos.position, Quaternion.identity);
            Fireball fireballEf = fireball.GetComponent<Fireball>();
            fireballEf.fireballLifetime = fireballLifetime;
            fireballEf.Fire(enemyManager);
            
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
