using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffAttackState : EnemyState
{
    public override EnemyState Tick(EnemyManager enemyManager){
        enemyManager.enemyAttacker.AttackTarget();
        return GetComponent<PursueState>();
    }
}
