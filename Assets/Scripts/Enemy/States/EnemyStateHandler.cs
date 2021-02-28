using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [HideInInspector]
    public EnemyManager enemyManager;

    [SerializeField]
    public EnemyState currentState;

    void Start(){
        enemyManager = GetComponent<EnemyManager>();

        currentState = GetComponentInChildren<IdleState>();
    }

    void FixedUpdate()
    {
        HandleCurrentState();
    }

    public void HandleCurrentState(){
        if(currentState != null){
            currentState = currentState.Tick(enemyManager);
        }
    }
}
