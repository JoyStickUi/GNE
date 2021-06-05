using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public GameObject movementRules;
    public GameObject rollRules;
    public GameObject lightAttackRules;
    public GameObject heavyAttackRules;

    bool movementFlag = false;
    bool rollFlag = false;
    bool lightAttackFlag = false;
    bool heavyAttackFlag = false;

    public void MovementTrigger(){
        if(!movementFlag){
            movementRules.SetActive(false);
            rollRules.SetActive(true);
            movementFlag = true;
        }
    }
    public void RollTrigger(){
        if(movementFlag && !rollFlag){
            rollRules.SetActive(false);
            lightAttackRules.SetActive(true);
            rollFlag = true;
        }
    }
    public void LightAttackTrigger(){
        if(rollFlag && !lightAttackFlag){
            lightAttackRules.SetActive(false);
            heavyAttackRules.SetActive(true);
            lightAttackFlag = true;
        }
    }
    public void HeavyAttackTrigger(){
        if(lightAttackFlag && !heavyAttackFlag){
            heavyAttackRules.SetActive(false);
            heavyAttackFlag = true;
        }
    }
}
