using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy phase")]
public class EnemyPhase : ScriptableObject
{
    public enum MAIN_STATES{
        Pursue,
        Idle,
        KeepOffset
    }
    public MAIN_STATES keyState ;
}
