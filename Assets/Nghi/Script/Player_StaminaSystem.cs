using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StaminaSystem : MonoBehaviour
{
    public int maxStamina = 100;
    public int currrentStamina;
    public Player_StaminaBar player_StaminaBar;
    // Start is called before the first frame update
    void Start()
    {
        currrentStamina = maxStamina;
        player_StaminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceStamina(int staminaAmount)
    {
        if (currrentStamina > staminaAmount)
        {
            currrentStamina -= staminaAmount;
        }


        if(currrentStamina < 0)
        {
            //Khong cho thuc hien don tan cong dac biet nua
        }
    }

    public void AddStamina(int staminaAmount)
    {
        if (currrentStamina < maxStamina)
        {
            currrentStamina += staminaAmount;
            currrentStamina = Mathf.Min(currrentStamina, maxStamina);
        }

    }
}
