using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : EffectInfluencer
{
    private int craterDamage = 100;

    public override void InfluenceEn(){
        playerManager.playerStats.TakeDamage(craterDamage);
    }

    public override void InfluenceSt(){
        
    }

    public override void InfluenceEx(){
        
    }

    public override void Reset()
    {
        
    }
}
