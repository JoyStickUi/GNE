using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLayerCollisionHandler : MonoBehaviour
{
    public PlayerManager playerManager;

    void Awake(){
        playerManager = GetComponent<PlayerManager>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Effect")){
            other.gameObject.GetComponent<EffectInfluencer>().InfluenceEn(playerManager);
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Effect")){
            other.gameObject.GetComponent<EffectInfluencer>().InfluenceSt(playerManager);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Effect")){
            other.gameObject.GetComponent<EffectInfluencer>().InfluenceEx(playerManager);
        }
    }
}
