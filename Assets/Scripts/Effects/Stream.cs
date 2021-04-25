using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : EffectInfluencer
{
    private int craterDamage = 80;

    public override void InfluenceEn(PlayerManager playerManager){
        playerManager.playerStats.TakeDamage(craterDamage);
    }

    public override void InfluenceSt(PlayerManager playerManager){
        
    }

    public override void InfluenceEx(PlayerManager playerManager){
        
    } 
}
