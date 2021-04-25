using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectInfluencer : MonoBehaviour
{
    public abstract void InfluenceEn(PlayerManager playerManager);
    public abstract void InfluenceSt(PlayerManager playerManager);
    public abstract void InfluenceEx(PlayerManager playerManager);
}
