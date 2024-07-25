using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_StaminaBar : MonoBehaviour
{
    public Slider stamina_Slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxStamina(int staminaAmount)
    {
        stamina_Slider.maxValue = staminaAmount;
        stamina_Slider.value = staminaAmount;
    }

    public void SetStamina(int staminaAmount)
    {
        stamina_Slider.value = staminaAmount;
    }
}
