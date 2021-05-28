using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangingState : EnemyState
{
    public EnemyPhase secondPhase;
    public GameObject firstPhaseModel;
    public GameObject SecondPhaseModel;
    public override EnemyState Tick(EnemyManager enemyManager)
    {
        enemyManager.currentPhase = secondPhase;
        firstPhaseModel.SetActive(false);
        SecondPhaseModel.SetActive(true);
        enemyManager.ReGetAnimHandler();
        return GetComponent<IdleState>();
    }
}
