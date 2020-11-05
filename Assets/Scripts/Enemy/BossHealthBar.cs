using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    Slider slider;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth){
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }
}

