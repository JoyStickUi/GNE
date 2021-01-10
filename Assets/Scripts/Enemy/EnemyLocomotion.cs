using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour
{    
    EnemyManager enemyManager;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public EnemyAnimatorHandler enemyAnimatorHandler;

    public new Rigidbody rigidbody;

    LayerMask ignoreForGroundCheck;

    [Header("Movement stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float fallingSpeed = 45;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        rigidbody = GetComponent<Rigidbody>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        myTransform = transform;
        enemyAnimatorHandler.Initialize();

        enemyManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
    }
    
    Vector3 normalVector;

    public void HandleMovementD(float delta){
        moveDirection = myTransform.forward;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;

        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        GetComponent<Rigidbody>().velocity = projectedVelocity;

        enemyAnimatorHandler.UpdateAnimatorValues(0, 0);

        if(enemyAnimatorHandler.canRotate){
            HandleRotation(delta);
        }
    }

    private void HandleRotation(float delta){        
        Vector3 playerPosition = enemyManager.behaviour_variables.playerTransform.position;
        Vector3 direction = playerPosition - myTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        direction.Normalize();
        direction.y = 0;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(direction);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }
}
