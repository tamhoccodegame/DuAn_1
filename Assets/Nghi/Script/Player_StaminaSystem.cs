using UnityEngine;

public class Player_StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100;
    public float currrentStamina;
    public int regenerateSpeed = 10;
    private Player_StaminaBar player_StaminaBar;
    // Start is called before the first frame update
    void Start()
    {
        player_StaminaBar = GameSession.instance.GetPlayer_StaminaBar();
        currrentStamina = maxStamina;
        Debug.Log(player_StaminaBar.name);
        player_StaminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        currrentStamina = Mathf.Clamp(currrentStamina + 15 * Time.deltaTime, 0, maxStamina);
        player_StaminaBar.SetStamina(currrentStamina);
	}

    public bool ReduceStamina()
    {
        if(currrentStamina == maxStamina)
        {
            currrentStamina = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
