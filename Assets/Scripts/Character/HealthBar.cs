using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    public Slider backSlider;
    int maxHealth;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth){
        maxHealth = maxHealth;
        if(slider){
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
        if(backSlider){
            backSlider.maxValue = maxHealth;
            backSlider.value = maxHealth;
        }
    }

    public void SetCurrentHealth(int currentHealth){
        if(slider) slider.value = currentHealth;
        if(backSlider) StartCoroutine(FadeBar(currentHealth));
    }

    IEnumerator FadeBar(float endValue){
        yield return new WaitForSeconds(1f);
        float time = 0f;
        while(time < 1f){
            time += 0.01f;
            backSlider.value = Mathf.Lerp(backSlider.value, endValue, time);
            yield return null;
        }
    }
}
