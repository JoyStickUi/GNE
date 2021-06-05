using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public GameObject prevDirector;
    public GameObject director;
    public override EnemyState Tick(EnemyManager enemyManager)
    {
        prevDirector.SetActive(false);
        director.SetActive(true);
        return this;
    }
}
