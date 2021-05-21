using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    Slider loadingBar;

    public IEnumerator LoadLevel(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
