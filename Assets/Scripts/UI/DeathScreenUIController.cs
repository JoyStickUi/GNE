using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenUIController : MonoBehaviour
{
    [SerializeField]
    LevelLoader levelLoader;
    [SerializeField]
    GameObject deathScreenBody;

    public void Show(){
        deathScreenBody.SetActive(true);
    }

    public void Retry(){
        Time.timeScale = 1;
        StartCoroutine(levelLoader.LoadLevel(1));
    }

    public void Exit(){
        Time.timeScale = 1;
        StartCoroutine(levelLoader.LoadLevel(0));
    }
}
