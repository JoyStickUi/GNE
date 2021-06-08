﻿using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    Transform cameraObject;
    InputHandler inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    public Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Ground and air detection stats")]
    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField]
    float groundDirectionRayDistance = 0.2f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;

    [Header("Movement stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float walkingSpeed = 1;
    [SerializeField]
    float sprintSpeed = 7;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float fallingSpeed = 45;
    public float movementSpeedModifier = 1f;

    [Header("Movement stamina cost")]
    [SerializeField]
    int sprintCost = 2;
    int rollCost = 30;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody>(); 
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();

        playerManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    public void ChangeSpeedModifier(float modifier){
        movementSpeedModifier = modifier;
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta){
        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta){
        if(inputHandler.rollFlag)
            return;

        if(playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed * movementSpeedModifier;

        if(inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f && sprintCost <= playerManager.playerStats.currentStamina){
            playerManager.playerStats.TakeStaminaDamage(sprintCost);
            speed = sprintSpeed * movementSpeedModifier;
            playerManager.isSprinting = true;
            moveDirection *= speed;
        }else{
            if(inputHandler.moveAmount < 0.5){
                moveDirection *= (walkingSpeed * movementSpeedModifier);
            }else{
                moveDirection *= speed;
            }
            playerManager.isSprinting = false;
        }

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

        if(animatorHandler.canRotate){
            HandleRotation(delta);
        }
    }

    public void HandleRolling(float delta){
        if(animatorHandler.anim.GetBool("isInteracting"))
            return;

        if(inputHandler.rollFlag && rollCost <= playerManager.playerStats.currentStamina){
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if(inputHandler.moveAmount > 0){
                playerManager.playerStats.TakeStaminaDamage(rollCost);
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }else{
                animatorHandler.PlayTargetAnimation("Damage", true); // makes step back from the wall
            }
        }
    }

    public void HandleFalling(float delta, Vector3 moveDirection){
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if(Physics.Raycast(origin, myTransform.forward, out hit, 0.4f)){
            moveDirection = Vector3.zero;
        }

        if(playerManager.isInAir){
            rigidbody.AddForce(-Vector3.up * fallingSpeed);
            rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck)){
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if(playerManager.isInAir){
                if(inAirTimer > 0.5f){
                    animatorHandler.PlayTargetAnimation("Land", true);
                }else{
                    animatorHandler.PlayTargetAnimation("Empty", false);                    
                }

                inAirTimer = 0;
                playerManager.isInAir = false;
            }
        }else{
            if(playerManager.isGrounded){
                playerManager.isGrounded = false;
            }
            
            if(!playerManager.isInAir){
                if(!playerManager.isInteracting){
                    animatorHandler.PlayTargetAnimation("Falling", true);
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
        }

        if(playerManager.isInteracting || inputHandler.moveAmount > 0){
            myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
        }else{
            myTransform.position = targetPosition;
        }

        if(playerManager.isGrounded){
            if(playerManager.isInteracting || inputHandler.moveAmount > 0){
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            }else{
                myTransform.position = targetPosition;
            }
        }
    }

    #endregion
}
