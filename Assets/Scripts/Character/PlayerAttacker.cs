using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    InputHandler inputHandler;
    public string lastAttack;

    private void Awake(){
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        inputHandler = GetComponent<InputHandler>();
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
        animatorHandler.PlayTargetAnimation(weapon.Light_Attack_1, true);
        lastAttack = weapon.Light_Attack_1;
    }

    public void HandleHeavyAttack(WeaponItem weapon){
        animatorHandler.PlayTargetAnimation(weapon.Heavy_Attack_1, true);
        lastAttack = weapon.Heavy_Attack_1;
    }
}
