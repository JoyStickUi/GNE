using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffAttackState : EnemyState
{
    public GameObject attackEffect;

    private IEnumerator ShowEffect(){
        attackEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackEffect.SetActive(false);
    }

    public override EnemyState Tick(EnemyManager enemyManager){
        enemyManager.enemyAttacker.AttackTarget();
        StartCoroutine(ShowEffect());
        return GetComponent<PursueState>();
    }
}
