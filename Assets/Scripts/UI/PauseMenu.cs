using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    LevelLoader levelLoader;
    bool isPause = false;

    public void PauseNResume(){
        isPause = !isPause;
        if(isPause){
            Time.timeScale = 0;
            ShowPauseMenu();
        }else{
            Time.timeScale = 1;
            HidePauseMenu();
        }
    }

    void ShowPauseMenu(){
        Cursor.visible = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void HidePauseMenu(){
        Cursor.visible = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Exit(){
        StartCoroutine(levelLoader.LoadLevel(0));
    }
}
