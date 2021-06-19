using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown settingList;
    void Start(){
        settingList.value = QualitySettings.GetQualityLevel();
    }

    public void SetQuality(int option){
        QualitySettings.SetQualityLevel(option);
    }
}
