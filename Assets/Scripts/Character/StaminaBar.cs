using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Slider slider;

    public Slider backSlider;
    int maxStamina;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    public void SetMaxStamina(int maxStamina){
        maxStamina = maxStamina;
        if(slider){
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
        if(backSlider){
            backSlider.maxValue = maxStamina;
            backSlider.value = maxStamina;
        }
    }

    public void SetCurrentStamina(int currentStamina){
        if(slider) slider.value = currentStamina;    
        if(backSlider) StartCoroutine(FadeBar(currentStamina));
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
