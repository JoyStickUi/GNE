using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorHandler : MonoBehaviour
{
    public Animator anim;
    EnemyLocomotion enemyLocomotion;

    private void Awake(){
        anim = GetComponent<Animator>();
        enemyLocomotion = GetComponentInParent<EnemyLocomotion>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    private void OnAnimatorMove(){
        float delta = Time.deltaTime;
        enemyLocomotion.rb.drag = 0;
        // Vector3 deltaPosition = anim.deltaPosition;
        // deltaPosition.y = 0;
        // Vector3 velocity = deltaPosition / delta;
        Vector3 velocity = (enemyLocomotion.targetTransform.position - transform.position).normalized;
        enemyLocomotion.rb.velocity = velocity * anim.GetFloat("Vertical");
    }
}
