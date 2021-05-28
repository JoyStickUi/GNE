using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollInvincibility : MonoBehaviour
{
    PlayerManager playerManager;

    void Start(){
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void EnableInv(){
        playerManager.playerStats.EnableInv();
    }

    public void DisableInv(){
        playerManager.playerStats.DisableInv();
    }
}
