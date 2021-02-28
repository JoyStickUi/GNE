using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI Enemy attack action")]
public class EnemyAttackAction : EnemyAction
{
    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximuxAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float maximumDistanceNeededToAttack = 3;
    public float minimumDistanceNeededToAttack = 0;
}
