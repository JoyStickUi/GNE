﻿using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 10;
    public int maxStamina;
    public int currentStamina;

    public HealthBar healthbar;
    public StaminaBar staminaBar;

    AnimatorHandler animatorHandler;

    private void Awake(){
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
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

    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);
        animatorHandler.PlayTargetAnimation("Damage", true);

        if(currentHealth <= 0){
            currentHealth = 0;
            animatorHandler.PlayTargetAnimation("Dead", false);
        }
    }

    public void TakeStaminaDamage(int damage){
        currentStamina = currentStamina - damage;
        staminaBar.SetCurrentStamina(currentStamina);

        if(currentStamina <= 0){
            currentStamina = 0;
        }
    }
}
