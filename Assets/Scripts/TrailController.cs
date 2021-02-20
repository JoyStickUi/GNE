using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    WeaponTrail _trail;

    void Update(){
        if(GetComponentInChildren<WeaponTrail>() != null){
            _trail = GetComponentInChildren<WeaponTrail>();
        }
    }

    public void EnableTrailEffect(){
        _trail.Emit = true;
    }
    
    public void DisableTrailEffect(){
        _trail.Emit = false;
    }
}
