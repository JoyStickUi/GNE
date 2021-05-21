using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{   
    [SerializeField]
    LevelLoader levelLoader;

    public void Play(){
        StartCoroutine(levelLoader.LoadLevel(1));
    }
    public void Exit(){
        Application.Quit();
    }
}
