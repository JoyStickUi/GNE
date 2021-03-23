using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampAttackState : EnemyState
{
    [SerializeField]
    private float minSwampSpawnDistance = 2f;
    [SerializeField]
    private float maxSwampSpawnDistance = 5f;
    [SerializeField]
    private GameObject swampPrefab;
    [SerializeField]
    private float swampLifetime = 1f;
    
    public override EnemyState Tick(EnemyManager enemyManager){
        if(swampPrefab != null){
            Vector3 swampPos = new Vector3(
                transform.position.x + Random.Range(minSwampSpawnDistance, maxSwampSpawnDistance) * Random.Range(-1f, 1f),
                transform.position.y,
                transform.position.y + Random.Range(minSwampSpawnDistance, maxSwampSpawnDistance) * Random.Range(-1f, 1f)
            );
            GameObject swamp = Instantiate(swampPrefab, swampPos, Quaternion.identity);
            Destroy(swamp, swampLifetime);
        }

        return GetComponent<IdleState>();
    }
}
