using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyLocomotion enemyLocomotion;
    public EnemyAttacker enemyAttacker;
    public EnemyAnimatorHandler enemyAnimatorHandler;
    public EnemyStateHandler enemyStateHandler;
    public EnemyStats enemyStats;
    public NeuralNetwork brain;

    private bool _isInteracting;
    private bool _isAttack;

    [Header("Phase neural layers")]
    List<NeuralLayer> firstPhase;
    List<NeuralLayer> secondPhase;

    public bool isInteracting{
        get{
            _isInteracting = enemyAnimatorHandler.anim.GetBool("isInteracting");
            return _isInteracting;
        }
        set{
            enemyAnimatorHandler.anim.SetBool("isInteracting", value);
            _isInteracting = value;
        }
    }

    public bool isAttack{
        get{
            _isAttack = enemyAnimatorHandler.anim.GetBool("isAttack");
            return _isAttack;
        }
        set{
            enemyAnimatorHandler.anim.SetBool("isAttack", value);
            _isAttack = value;
        }
    }

    [Header("Target settings")]
    public Transform lockOnTransform;

    public PlayerStats currentTarget = null;
    public Transform targetTransform = null;

    [Header("Phase settings")]
    public EnemyPhase currentPhase;

    void Awake(){
        brain = new NeuralNetwork();
    }

    void Start(){
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttacker = GetComponent<EnemyAttacker>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        enemyStateHandler = GetComponent<EnemyStateHandler>();
        enemyStats = GetComponent<EnemyStats>();

        ParseDatasets();
        LoadFirstPhaseDataset();
    }

    void ParseDatasets(){
        brain.AddLayer(4, 10);
        brain.AddLayer(10, 3);
        firstPhase = JsonUtility
                .FromJson<NetworkData>(Resources.Load<TextAsset>("firstPhase").text)
                .FromJson();
        secondPhase = JsonUtility
                .FromJson<NetworkData>(Resources.Load<TextAsset>("secondPhase").text)
                .FromJson(); 
    }

    public void LoadFirstPhaseDataset(){
        brain.ClearLayerList();
        brain.AddLayer(4, 10);
        brain.AddLayer(10, 3);
        brain.LoadTrainedLayers(firstPhase);
    }

    public void LoadSecondPhaseDataset(){
        brain.ClearLayerList();
        brain.AddLayer(1, 10);
        brain.AddLayer(10, 2);
        brain.LoadTrainedLayers(secondPhase);
    }

    public void ReGetAnimHandler(){
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }

    void Update(){
    
    }
}
