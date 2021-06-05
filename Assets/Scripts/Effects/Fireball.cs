using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EffectInfluencer
{
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private float fireballSpeed = 5f;
    public float fireballLifetime = 9f;

    public override void InfluenceEn(){
        playerManager.playerStats.TakeDamage(damage);
    }

    public override void InfluenceSt(){
        
    }

    public override void InfluenceEx(){
        
    }

    void Start(){
        Destroy(this.transform.gameObject, fireballLifetime);
    }

    public void Fire(EnemyManager enemyManager){
        Vector3 velocity = (enemyManager.targetTransform.transform.position - transform.position).normalized;
        velocity.y = 0f;
        GetComponent<Rigidbody>().velocity = (velocity * fireballSpeed);
    }

    public override void Reset()
    {
        
    }
}
