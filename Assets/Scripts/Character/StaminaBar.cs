using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Slider slider;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    public void SetMaxStamina(int maxStamina){
        if(slider){
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
    }

    public void SetCurrentStamina(int currentStamina){
        if(slider) slider.value = currentStamina;
    }
}
