using UnityEngine;
using UnityEngine.Playables;

public class InputHandler : MonoBehaviour
{
    public InstructionsController instructionsController;

    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool lockOnInput;

    public bool pauseFlag;
    public bool rollFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public float rollInputTimer;

    PlayerControls inputActions;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    CameraHandler cameraHandler; 
    PauseMenu pauseMenu;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake(){
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void OnEnable(){
        if(inputActions == null){
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            inputActions.PlayerPause.pauseKey.performed += i => pauseFlag = true;
        }

        inputActions.Enable();
    }

    private void OnDisable(){
        inputActions.Disable();
    }

    public void TickInput(float delta){
        HandlePause();
        MoveInput(delta);
        HandleRollInput(delta);
        HandleAttackInput(delta);
        HandleLockOnInput();
    }

    private void MoveInput(float delta){
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
        if(moveAmount > 0.5f){
            instructionsController.MovementTrigger();
        }
    }

    private void HandleRollInput(float delta){
        b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        sprintFlag = b_Input;

        if(b_Input){            
            rollInputTimer += delta;
            sprintFlag = true;
        }else{
            if(rollInputTimer > 0 && rollInputTimer < 0.5f){
                sprintFlag = false;
                rollFlag = true;
                instructionsController.RollTrigger();
            }

            rollInputTimer = 0;
        }
    }

    private void HandleAttackInput(float delta){
        if(rb_Input){
            if(playerManager.canCombo){
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
            }else{
                if(playerManager.isInteracting)
                    return;
                    
                if(playerManager.canCombo)
                    return;

                instructionsController.LightAttackTrigger();
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
            
        }

        if(rt_Input){
            instructionsController.HeavyAttackTrigger();
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

    private void HandleLockOnInput(){
        if(lockOnInput && lockOnFlag == false){
            cameraHandler.ClearLockOnTargets();
            lockOnInput = false;            
            cameraHandler.HandleLockOn();
            if(cameraHandler.nearestLockOnTarget != null){
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }
        }else if(lockOnInput && lockOnFlag){
            lockOnInput = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }
    }

    private void HandlePause(){
        if(pauseFlag){
            pauseMenu.PauseNResume();
            pauseFlag = false;
        }
    }
}
