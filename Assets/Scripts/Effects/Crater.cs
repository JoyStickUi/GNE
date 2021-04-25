using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : EffectInfluencer
{
    private int craterDamage = 100;

    public override void InfluenceEn(PlayerManager playerManager){
        playerManager.playerStats.TakeDamage(craterDamage);
    }

    public override void InfluenceSt(PlayerManager playerManager){
        
    }

    public override void InfluenceEx(PlayerManager playerManager){
        
    } 
}
