using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("One handed attack animations")]
    public string Light_Attack_1;
    public string Heavy_Attack_1;
    public string Light_Attack_2;

    [Header("Strike costs")]
    public int baseDamage;
    public float lightDamageAttackMultiplier;
    public float heavyDamageAttackMultiplier;

    [Header("Stamina Costs")]
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;
}
