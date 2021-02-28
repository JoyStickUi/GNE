using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorHandler : MonoBehaviour
{
    EnemyLocomotion enemyLocomotion;
    [HideInInspector]
    public EnemyManager enemyManager;

    public Animator anim;

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
        Vector3 velocity = Vector3.zero;

        if(enemyManager.targetTransform != null){
            velocity = (enemyManager.targetTransform.position - transform.position).normalized;
        }

        enemyLocomotion.rb.velocity = velocity * anim.GetFloat("Vertical");
    }
}
