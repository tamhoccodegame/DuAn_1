using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Mana : MonoBehaviour
{
    private Slider skillSlider;
    public float maxSkillMana = 100f;
    public float currentSkillMana;
    public float skillCost;
    // Start is called before the first frame update
    void Start()
    {
        skillSlider = GameSession.instance.GetPlayer_SkillBar();
        currentSkillMana = maxSkillMana;
        skillSlider.maxValue = maxSkillMana;
        skillSlider.value = currentSkillMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool UseSkill(float cost)
    {
        if (currentSkillMana >= cost)
        {
            currentSkillMana -= cost;
            skillSlider.value = currentSkillMana;
            SetSkillMana(currentSkillMana);
            return true;
        }
        else
        {
            Debug.Log("Not enough skill mana!");
            return false;
        }
    }

    public void AddSkillMana(float amount)
    {
        currentSkillMana = Mathf.Clamp(currentSkillMana + amount, 0, maxSkillMana);
        skillSlider.value = currentSkillMana;
        SetSkillMana(currentSkillMana);
    }

    public void SetMaxSkillMana(float amount)
    {
        skillSlider.maxValue = amount;
        skillSlider.value = amount;
    }

    public void SetSkillMana(float amount)
    {
        skillSlider.value = amount;
    }
}
