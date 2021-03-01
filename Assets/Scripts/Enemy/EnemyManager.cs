using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyLocomotion enemyLocomotion;
    public EnemyAttacker enemyAttacker;
    public EnemyAnimatorHandler enemyAnimatorHandler;
    public EnemyStateHandler enemyStateHandler;
    public EnemyStats enemyStats;

    private bool _isInteracting;

    public bool isInteracting{
        get{
            _isInteracting = enemyAnimatorHandler.anim.GetBool("isInteracting");
            return _isInteracting;
        }
        set{
            enemyAnimatorHandler.anim.SetBool("isInteracting", value);
            _isInteracting = value;
        }
    }

    [Header("Target settings")]
    public Transform lockOnTransform;

    public CharacterStats currentTarget = null;
    public Transform targetTransform = null;

    [Header("Phase settings")]
    public EnemyPhase currentPhase;

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttacker = GetComponent<EnemyAttacker>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        enemyStateHandler = GetComponent<EnemyStateHandler>();
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update(){
    
    }
}
