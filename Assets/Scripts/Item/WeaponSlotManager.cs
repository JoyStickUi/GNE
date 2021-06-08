using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponItem attackingWeapon;

    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;
    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    PlayerStats playerStats;

    private void Awake(){
        playerStats = GetComponentInParent<PlayerStats>();

        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots){
            if(weaponSlot.isLeftHandSlot){
                leftHandSlot = weaponSlot;
            }else if(weaponSlot.isRightHandSlot){
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft){
        if(isLeft){
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
        }else{
            rightHandSlot.LoadWeaponModel(weaponItem);
            attackingWeapon = weaponItem;
            rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>().currentWeaponDamage = weaponItem.baseDamage;
            LoadRightWeaponDamageCollider();
        }
    }

    public void DrainStaminaLightAttack(){
        playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
    }

    public void DrainStaminaHeavyAttack(){
        playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
    }

    private void LoadLeftWeaponDamageCollider(){
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider(){
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        float multiplier = 1f;
        if(playerStats.playerManager.inputHandler.rb_Input){
            multiplier = attackingWeapon.lightDamageAttackMultiplier;
        }else if(playerStats.playerManager.inputHandler.rt_Input){
            multiplier = attackingWeapon.heavyDamageAttackMultiplier;
        }
        rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>().damageMultiplier = multiplier;
    }

    public void OpenRightDamageCollider(){
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftDamageCollider(){
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightHandDamageCollider(){
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftHandDamageCollider(){
        leftHandDamageCollider.DisableDamageCollider();
    }
}
