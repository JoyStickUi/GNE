using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipControl : MonoBehaviour
{
    PlayerControls inputActions;

    public bool skipFlag;

    public PlayableDirector _startCutDirector;
    public PlayableDirector _enemyCutDirector;

    void Start(){
        if(inputActions == null){
            inputActions = new PlayerControls();
            inputActions.PlayerPause.pauseKey.performed += i => skipFlag = true;
        }

        inputActions.Enable();
    }

    void Update(){
        HandleSkip();
    }

    public void HandleSkip(){
        if(skipFlag){
            if(_startCutDirector.isActiveAndEnabled){
                _startCutDirector.time = 1062f;
            }

            if(_enemyCutDirector.isActiveAndEnabled){
                _enemyCutDirector.time = 360f;
            }

            skipFlag = false;
        }
    }
}
