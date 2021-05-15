using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : EffectInfluencer
{
    private float damageCooldownTime = 1f;
    private float damageCooldownTimer;
    public float swampLifetime = 9f;
    public int damagePerSecond = 1;

    void Start(){
        damageCooldownTimer = damageCooldownTime;
        Destroy(this.transform.gameObject, swampLifetime);
    }

    public override void Reset(){
        playerManager.playerLocomotion.ChangeSpeedModifier(1f);
    }

    public override void InfluenceEn(){
        playerManager.playerLocomotion.ChangeSpeedModifier(0.25f);
    }

    public override void InfluenceSt(){
        playerManager.playerLocomotion.ChangeSpeedModifier(0.25f);
        damageCooldownTimer -= Time.deltaTime;
        if(damageCooldownTimer < 0f){
            playerManager.playerStats.TakeDamage(damagePerSecond);
            damageCooldownTimer = damageCooldownTime;
        }
    }

    public override void InfluenceEx(){
        if(playerManager != null)
            Reset();
    }

    void OnDestroy(){
        if(playerManager != null)
            Reset();
    }
}
