using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectInfluencer : MonoBehaviour
{
    protected PlayerManager playerManager;
    public abstract void InfluenceEn();
    public abstract void InfluenceSt();
    public abstract void InfluenceEx();
    public abstract void Reset();
    public void SetManager(PlayerManager plm)
    {
        playerManager = plm;
    }
}
