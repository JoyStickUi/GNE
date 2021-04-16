using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectInfluencer : MonoBehaviour
{
    public abstract void Influence(PlayerManager playerManager);
}
