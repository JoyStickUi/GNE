using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Target settings")]
    public Transform lockOnTransform;

    public EnemyLocomotion enemyLocomotion;
    public EnemyAttacker enemyAttacker;
    public EnemyAnimatorHandler enemyAnimatorHandler;
    public EnemyStateHandler enemyStateHandler;

    public bool isInteracting;

    public CharacterStats currentTarget = null;
    public Transform targetTransform = null;

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttacker = GetComponent<EnemyAttacker>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        enemyStateHandler = GetComponent<EnemyStateHandler>();

        enemyLocomotion.enemyManager = this;
        enemyAttacker.enemyManager = this;
        enemyAnimatorHandler.enemyManager = this;
        enemyStateHandler.enemyManager = this;
    }

    void Update(){
        // isInteracting = anim.GetBool("isInteracting");
    }
}
