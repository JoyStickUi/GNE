using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform lockOnTransform;
    public BehaviourPubVariables behaviour_variables;

    Animator anim;
    EnemyLocomotion enemyLocomotion;

    public bool isInteracting;
    public bool isInAir;
    public bool isGrounded;

    [Header("Rage distance")]
    public float minDistance;
    public float maxDistance;

    void Start(){
        minDistance = 2f;
        maxDistance = 10f;

        anim = GetComponentInChildren<Animator>();
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        behaviour_variables = GetComponentInChildren<BehaviourPubVariables>();
    }

    private void FixedUpdate(){
        float delta = Time.fixedDeltaTime;
        
        // enemyLocomotion.HandleMovement(delta);
        // enemyLocomotion.HandleFalling(delta, enemyLocomotion.moveDirection);
    }

    void Update(){
        isInteracting = anim.GetBool("isInteracting");
    }
}
