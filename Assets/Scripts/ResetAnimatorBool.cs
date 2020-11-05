using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public List<string> targets;
    public bool status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        foreach(string targetBool in targets){
            animator.SetBool(targetBool, status);
        }
    }
}
