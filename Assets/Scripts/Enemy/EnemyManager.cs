using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform lockOnTransform;

    Animator anim;
    EnemyLocomotion enemyLocomotion;
    EnemyAnimatorHandler enemyAnimatorHandler;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public bool isInteracting;
    public bool isInAir;
    public bool isGrounded;

    [Header("AI settings")]
    public float detectionRadius;
    public float maximumDetectionAngle = 50f;
    public float minimumDetectionAngle = -50f;

    public float currentRecoveryTime = 0;

    void Start(){
        anim = GetComponentInChildren<Animator>();
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    private void FixedUpdate(){
        float delta = Time.fixedDeltaTime;
        
        HandleCurrentAction();
    }

    private void HandleCurrentAction(){
        if(enemyLocomotion.currentTarget == null){
            enemyLocomotion.HandleDetection();
        }

        if(enemyLocomotion.currentTarget != null){
            enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyLocomotion.currentTarget.transform.position, transform.position);

            enemyLocomotion.HandleMoveToTarget();  
            
            if(enemyLocomotion.distanceFromTarget <= enemyLocomotion.stoppingDistance){
                //handle attack action
                AttackTarget();
            }
        }
    }

    void Update(){
        // isInteracting = anim.GetBool("isInteracting");
        HandleRecoveryTimer();
    }

    private void AttackTarget(){
        if(isInteracting)
            return;

        if(currentAttack == null){
            GetNewAttack();
        }else{
            isInteracting = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    private void GetNewAttack(){
        Vector3 targetDirection = enemyLocomotion.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        enemyLocomotion.distanceFromTarget = Vector3.Distance(enemyLocomotion.currentTarget.transform.position, transform.position);

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

        if(isInteracting && currentRecoveryTime <= 0){
            isInteracting = false;
        }
    }
}
