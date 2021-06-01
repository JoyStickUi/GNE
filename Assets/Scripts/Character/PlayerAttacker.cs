using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    InputHandler inputHandler;
    PlayerManager playerManager;
    public string lastAttack;

    private void Awake(){
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        inputHandler = GetComponent<InputHandler>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void HandleWeaponCombo(WeaponItem weapon){
        if(inputHandler.comboFlag){
            animatorHandler.anim.SetBool("canCombo", false);

            if(lastAttack == weapon.Light_Attack_1){                
                animatorHandler.PlayTargetAnimation(weapon.Light_Attack_2, true);
            }            
        }
    }

    public void HandleLightAttack(WeaponItem weapon){
        if((weapon.baseStamina * weapon.lightAttackMultiplier) <= playerManager.playerStats.currentStamina){
            animatorHandler.PlayTargetAnimation(weapon.Light_Attack_1, true);
            lastAttack = weapon.Light_Attack_1;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon){
        if((weapon.baseStamina * weapon.heavyAttackMultiplier) <= playerManager.playerStats.currentStamina){
            animatorHandler.PlayTargetAnimation(weapon.Heavy_Attack_1, true);
            lastAttack = weapon.Heavy_Attack_1;
        }
    }
}
