using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{    
    NavMeshAgent navMeshAgent;
    public Rigidbody rb;

    EnemyManager enemyManager;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public EnemyAnimatorHandler enemyAnimatorHandler;

    LayerMask ignoreForGroundCheck;
    public LayerMask detectionLayer;

    public CharacterStats currentTarget = null;
    public Transform targetTransform = null;

    [Header("Movement stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 15;
    [SerializeField]
    float fallingSpeed = 45;

    public float distanceFromTarget;
    public float stoppingDistance = 2f;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
    }

    void Start()
    {
        myTransform = transform;

        navMeshAgent.enabled = false;
        rb.isKinematic = false;

        enemyManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    public void HandleDetection(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; ++i){
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            Transform characterTransform = colliders[i].transform.GetComponent<Transform>();

            if(characterStats != null){
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle){
                    currentTarget = characterStats;
                    targetTransform = characterTransform;
                }
            }
        }
    }

    public void HandleMoveToTarget(){
        // if(enemyManager.isInteracting)
        //     return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if(enemyManager.isInteracting){
            enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }else{
            if(distanceFromTarget > stoppingDistance){
                enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            else if(distanceFromTarget <= stoppingDistance){
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget(){  
        if(enemyManager.isInteracting){
            Vector3 direction = currentTarget.transform.position - transform.position;
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
            navMeshAgent.SetDestination(currentTarget.transform.position);
            rb.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }
}
