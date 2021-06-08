using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : EffectInfluencer
{
    public int craterDamage = 80;

    public override void InfluenceEn(){
        playerManager.playerStats.TakeDamage(craterDamage);
    }

    public override void InfluenceSt(){
        
    }

    public override void InfluenceEx(){
        
    } 

    public override void Reset(){

    }
}
