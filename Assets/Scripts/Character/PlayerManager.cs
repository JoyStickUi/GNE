using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform lockOnTransform;

    public Rigidbody rb;

    public InputHandler inputHandler;
    CameraHandler cameraHandler;
    public PlayerLocomotion playerLocomotion;
    public PlayerInventory playerInventory;
    public PlayerAttacker playerAttacker;
    public PlayerStats playerStats;
    public AnimatorHandler animatorHandler;

    public bool _isInteracting;
    public bool isInteracting{
        get{
            _isInteracting = animatorHandler.anim.GetBool("isInteracting");
            return _isInteracting;
        }
        set{
            animatorHandler.anim.SetBool("isInteracting", value);
            _isInteracting = value;
        }
    }
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
        Cursor.visible = false;
        QualitySettings.vSyncCount = 1;
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInventory = GetComponent<PlayerInventory>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerStats = GetComponent<PlayerStats>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animatorHandler.anim.GetBool("isInteracting");
        canCombo = animatorHandler.anim.GetBool("canCombo");

        inputHandler.TickInput(delta); 
        playerLocomotion.HandleRolling(delta);       
    }

    private void FixedUpdate(){
        float delta = Time.fixedDeltaTime;

        if(cameraHandler != null){
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }

        if(!isInteracting){
            playerStats.RegenerateStamina();
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
