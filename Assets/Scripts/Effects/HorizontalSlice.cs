using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlice : EffectInfluencer
{
    [SerializeField]
    private int damage = 100;
    public override void InfluenceEn(){
        playerManager.playerStats.TakeDamage(damage);
        Vector3 dir = (transform.position - playerManager.playerLocomotion.myTransform.position);
        dir.y = 10f;
        playerManager.rb.AddForce(dir * 10f, ForceMode.Impulse);
    }

    public override void InfluenceSt(){
        
    }

    public override void InfluenceEx(){
        
    }

    public override void Reset()
    {
        
    }
}
