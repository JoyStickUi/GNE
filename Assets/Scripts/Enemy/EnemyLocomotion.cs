using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{    
    NavMeshAgent navMeshAgent;
    public Rigidbody rb;

    [HideInInspector]
    public EnemyManager enemyManager;

    [SerializeField]
    float rotationSpeed = 15;

    public float distanceFromTarget;
    public float stoppingDistance = 2f;
    public float keepOffsetDistance = 5f;
    public Vector3 figthZoneCenter;
    public float figthZoneRadius = 1f;

    void Awake(){
        enemyManager = GetComponent<EnemyManager>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
    }

    public void HandleMoveToTarget(){
        if(enemyManager.isInteracting){
            enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }else{
            distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            if(distanceFromTarget > stoppingDistance){
                enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            else if(distanceFromTarget <= stoppingDistance){
                enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    public void HandleKeepOffsetFromTarget(){
        if(enemyManager.isInteracting){
            enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }else{
            distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            if(distanceFromTarget < keepOffsetDistance){
                TeleportIfHitted();
                enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", -1, 0.1f, Time.deltaTime);
            }
            else if(distanceFromTarget >= keepOffsetDistance){
                enemyManager.enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void TeleportIfHitted(){
        if(enemyManager.enemyStats.isHitted){
            Vector3 randomPosition = figthZoneCenter;
            randomPosition.x += Random.Range(-figthZoneRadius, figthZoneRadius);
            randomPosition.z += Random.Range(-figthZoneRadius, figthZoneRadius);

            float calculatedY = Terrain.activeTerrain.SampleHeight(randomPosition);

            randomPosition.y = calculatedY;

            if(calculatedY > 9f && calculatedY < 11f){
                transform.position = randomPosition;
            }else{
                transform.position = figthZoneCenter;
            }
            enemyManager.enemyStats.isHitted = false;
        }
    }

    private void HandleRotateTowardsTarget(){  
        if(enemyManager.isInteracting){
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero){
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
        }else{
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = rb.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            rb.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }
}
