using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    private float damageCooldownTime = 1f;
    private float damageCooldownTimer;

    void Start(){
        damageCooldownTimer = damageCooldownTime;
    }

    void OnTriggerEnter(Collider other){
        other.gameObject.GetComponent<PlayerLocomotion>().ChangeSpeedModifier(0.25f);
    }

    void OnTriggerStay(Collider other){
        damageCooldownTimer -= Time.deltaTime;
        if(damageCooldownTimer < 0f){
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(1);
            damageCooldownTimer = damageCooldownTime;
        }
    }

    void OnTriggerExit(Collider other){
        other.gameObject.GetComponent<PlayerLocomotion>().ChangeSpeedModifier(1f);
    }
}
