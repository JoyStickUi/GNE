﻿using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform lockOnTransform;

    InputHandler inputHandler;
    CameraHandler cameraHandler;
    public PlayerLocomotion playerLocomotion;
    public PlayerInventory playerInventory;
    public PlayerAttacker playerAttacker;
    public PlayerStats playerStats;
    public AnimatorHandler animatorHandler;

    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canCombo;

    private void Awake(){
        cameraHandler = FindObjectOfType<CameraHandler>();
    }
    
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInventory = GetComponent<PlayerInventory>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerStats = GetComponent<PlayerStats>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animatorHandler.anim.GetBool("isInteracting");
        canCombo = animatorHandler.anim.GetBool("canCombo");

        inputHandler.TickInput(delta); 
        playerLocomotion.HandleRollingAndSprinting(delta);       
    }

    private void FixedUpdate(){
        float delta = Time.fixedDeltaTime;

        if(cameraHandler != null){
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }

        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
    }

    private void LateUpdate(){
        inputHandler.rollFlag = false;        
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;

        if(isInAir){
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
    }
}
