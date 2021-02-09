using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform lockOnTransform;

    Animator anim;
    EnemyLocomotion enemyLocomotion;

    public bool isInteracting;
    public bool isInAir;
    public bool isGrounded;

    [Header("AI settings")]
    public float detectionRadius;
    public float maximumDetectionAngle = 50f;
    public float minimumDetectionAngle = -50f;

    void Start(){
        anim = GetComponentInChildren<Animator>();
        enemyLocomotion = GetComponent<EnemyLocomotion>();
    }

    private void FixedUpdate(){
        float delta = Time.fixedDeltaTime;
        
        HandleCurrentAction();
    }

    private void HandleCurrentAction(){
        if(enemyLocomotion.currentTarget == null){
            enemyLocomotion.HandleDetection();
        }else{
            enemyLocomotion.HandleMoveToTarget();
        }
    }

    void Update(){
        isInteracting = anim.GetBool("isInteracting");
    }
}
