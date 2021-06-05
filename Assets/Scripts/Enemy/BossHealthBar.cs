using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    Slider slider;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth){
        if(slider){
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
    }

    public void SetCurrentHealth(int currentHealth){
        if(slider)slider.value = currentHealth;
    }
}

