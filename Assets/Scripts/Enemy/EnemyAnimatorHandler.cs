using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorHandler : MonoBehaviour
{
    EnemyManager enemyManager;
    public Animator anim;
    EnemyLocomotion enemyLocomotion;
    int vertical;
    int horizontal;
    public bool canRotate;

    public void Initialize(){
        enemyManager = GetComponentInParent<EnemyManager>();
        anim = GetComponent<Animator>();
        enemyLocomotion = GetComponentInParent<EnemyLocomotion>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement){
        #region Vertical
        float v = 0;
        if(verticalMovement > 0 && verticalMovement < 0.55f){
            v = 0.5f;
        }else if(verticalMovement > 0.55f){
            v = 1;
        }else if(verticalMovement < 0 && verticalMovement > -0.55f){
            v = -0.5f;
        }else if(verticalMovement < -0.55f){
            v = -1;
        }else{
            v= 0;
        }
        #endregion

        #region Horizontal
        float h = 0;

        if(horizontalMovement > 0 && horizontalMovement < 0.55f){
            h = 0.5f;
        }else if(horizontalMovement > 0.55f){
            h = 1;
        }else if(horizontalMovement < 0 && horizontalMovement > -0.55f){
            h = -0.5f;
        }else if(horizontalMovement < -0.55f){
            h = -1;
        }else{
            h = 0;
        }
        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void CanRotate(){
        canRotate = true;
    }

    public void StopRotation(){
        canRotate = false;
    }

    public void EnableCombo(){
        anim.SetBool("canCombo", true);
    }

    public void DisableCombo(){
        anim.SetBool("canCombo", false);
    }

    // private void OnAnimatorMove(){
    //     if(enemyManager.isInteracting == false)
    //         return;

    //     float delta = Time.deltaTime;
    //     enemyLocomotion.GetComponent<Rigidbody>().drag = 0;
    //     Vector3 deltaPosition = anim.deltaPosition;
    //     deltaPosition.y = 0;
    //     Vector3 velocity = deltaPosition / delta;
    //     enemyLocomotion.GetComponent<Rigidbody>().velocity = velocity;
    // }
}
