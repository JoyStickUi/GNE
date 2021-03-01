using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangingState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager)
    {
        return this;
    }
}
