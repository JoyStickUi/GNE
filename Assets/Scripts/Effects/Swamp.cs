using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : EffectInfluencer
{
    private float damageCooldownTime = 1f;
    private float damageCooldownTimer;
    private int damagePerSecond = 1;

    void Start(){
        damageCooldownTimer = damageCooldownTime;
    }

    public override void InfluenceEn(PlayerManager playerManager){
        playerManager.playerLocomotion.ChangeSpeedModifier(0.25f);
    }

    public override void InfluenceSt(PlayerManager playerManager){
        damageCooldownTimer -= Time.deltaTime;
        if(damageCooldownTimer < 0f){
            playerManager.playerStats.TakeDamage(damagePerSecond);
            damageCooldownTimer = damageCooldownTime;
        }
    }

    public override void InfluenceEx(PlayerManager playerManager){
        playerManager.playerLocomotion.ChangeSpeedModifier(1f);
    }
}
