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
        CapsuleCollider collider = GetComponentInParent<CapsuleCollider>();
        collider.center = new Vector3(collider.center.x, 1.2f, collider.center.z);
        collider.radius = 1.2f;
        enemyManager.ReGetAnimHandler();
        return GetComponent<IdleState>();
    }
}
