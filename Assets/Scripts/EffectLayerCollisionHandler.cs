using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLayerCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collision other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Effect")){
            
        }
    }

    void OnParticleCollision(GameObject other){
        
    }
}
