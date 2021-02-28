using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [HideInInspector]
    public EnemyManager enemyManager;
    EnemyLocomotion enemyLocomotion;
    EnemyAnimatorHandler enemyAnimatorHandler;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public float currentRecoveryTime = 0;

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    void Update()
    {
        HandleRecoveryTimer();
    }

    public void AttackTarget(){
        if(enemyManager.isInteracting)
            return;

        if(currentAttack == null){
            GetNewAttack();
        }else{
            enemyManager.isInteracting = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    private void GetNewAttack(){
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for(int i = 0; i < enemyAttacks.Length; ++i){
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(
                enemyLocomotion.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotion.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack
                && viewableAngle <= enemyAttackAction.maximuxAttackAngle
                && viewableAngle >= enemyAttackAction.minimumAttackAngle
            ){
                maxScore += enemyAttackAction.attackScore;
            }
        }

        int randValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for(int i = 0; i < enemyAttacks.Length; ++i){
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(
                enemyLocomotion.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotion.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack
                && viewableAngle <= enemyAttackAction.maximuxAttackAngle
                && viewableAngle >= enemyAttackAction.minimumAttackAngle
            ){
                if(currentAttack != null)
                    return;

                temporaryScore += enemyAttackAction.attackScore;

                if(temporaryScore > randValue){
                    currentAttack = enemyAttackAction;
                }
            }
        }
    }

    private void HandleRecoveryTimer(){
        if(currentRecoveryTime > 0){
            currentRecoveryTime -= Time.deltaTime;
        }

        if(enemyManager.isInteracting && currentRecoveryTime <= 0){
            enemyManager.isInteracting = false;
        }
    }
}
