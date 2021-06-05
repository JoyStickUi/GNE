using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 10;
    public int maxStamina;
    public float currentStamina;
    public float staminaRegenerationAmount = 0.1f;

    public HealthBar healthbar;
    public StaminaBar staminaBar;

    bool isInvincible = false;

    AnimatorHandler animatorHandler;
    PlayerDeathHandler playerDeathHandler;
    public PlayerManager playerManager;

    private void Awake(){
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        playerDeathHandler = GetComponent<PlayerDeathHandler>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    private int SetMaxHealthFromHealthLevel(){
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private int SetMaxStaminaFromStaminaLevel(){
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public void EnableInv(){
        isInvincible = true;
    }

    public void DisableInv(){
        isInvincible = false;
    }

    public void TakeDamage(int damage){
        if(!isInvincible){
            currentHealth = currentHealth - damage;
            healthbar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayTargetAnimation("Damage", true);

            if(currentHealth <= 0){
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead", false);
                playerDeathHandler.Handle();
            }
        }
    }

    public void TakeStaminaDamage(int damage){
        currentStamina = currentStamina - damage;
        staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));

        if(currentStamina <= 0){
            currentStamina = 0;
        }
    }

    public void RegenerateStamina(){
        if(currentStamina < maxStamina){
            currentStamina += staminaRegenerationAmount;
            staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
        }
    }
}
