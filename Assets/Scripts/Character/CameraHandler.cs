using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    InputHandler inputHandler;

    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandler singleton;
    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffset = 0.2f;

    public Transform currentLockOnTarget;

    List<EnemyManager> availableTargets = new List<EnemyManager>();
    public Transform nearestLockOnTarget;
    public float maximumLockOnDistance = 30;

    public void Awake(){
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);        
        inputHandler = FindObjectOfType<InputHandler>();
    }

    public void FollowTarget(float delta){
        Vector3 targetPosition = Vector3.SmoothDamp
            (myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput){
        if(inputHandler.lockOnFlag == false && currentLockOnTarget == null){
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }else{
            Vector3 dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            dir = currentLockOnTarget.position - cameraPivotTransform.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;
        }
    }

    public void HandleCameraCollisions(float delta){
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast
            (
                cameraPivotTransform.position,
                cameraSphereRadius,
                direction,
                out hit,
                Mathf.Abs(targetPosition),
                ignoreLayers
            )
        ){
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffSet);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset){
            targetPosition = -minimumCollisionOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }

    public void HandleLockOn(){
        float shortestDistance = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

        for(int i = 0; i < colliders.Length; ++i){
            EnemyManager enemy = colliders[i].GetComponent<EnemyManager>();

            if(enemy != null){
                Vector3 lockTargetDirection = enemy.transform.position - transform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, enemy.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                if(
                    enemy.transform.root != targetTransform.transform.root 
                    && viewableAngle > -50 
                    && viewableAngle < 50 
                    && distanceFromTarget <= maximumLockOnDistance
                ){
                    availableTargets.Add(enemy);
                }
            }
        }

        for(int i = 0; i < availableTargets.Count; ++i){
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[i].transform.position);

            if(distanceFromTarget < shortestDistance){
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[i].lockOnTransform;
            }
        }
    }

    public void ClearLockOnTargets(){
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }
}
