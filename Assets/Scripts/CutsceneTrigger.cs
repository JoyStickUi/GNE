using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject prevDirector;
    public GameObject director;
    
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.layer == LayerMask.NameToLayer("Character")){
            prevDirector.SetActive(false);
            director.SetActive(true);
            Destroy(this);
        }
    }
}
