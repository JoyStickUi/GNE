using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{   
    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    Slider loadingBar;

    public void Play(){
        StartCoroutine(LoadLevel());
    }
    public void Exit(){
        Application.Quit();
    }

    IEnumerator LoadLevel(){
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
