using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlice : EffectInfluencer
{
    [SerializeField]
    private int damage = 100;
    public override void InfluenceEn(){
        playerManager.playerStats.TakeDamage(damage);
    }

    public override void InfluenceSt(){
        
    }

    public override void InfluenceEx(){
        
    }

    public override void Reset()
    {
        
    }
}
