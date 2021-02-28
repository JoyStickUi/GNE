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
    public EnemyStats enemyStats;

    private bool _isInteracting;

    public bool isInteracting{
        get => _isInteracting;
        set{
            enemyAnimatorHandler.anim.SetBool("isInteracting", value);
            _isInteracting = value;
        }
    }

    public CharacterStats currentTarget = null;
    public Transform targetTransform = null;

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttacker = GetComponent<EnemyAttacker>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        enemyStateHandler = GetComponent<EnemyStateHandler>();
    }

    void Update(){
    
    }
}
