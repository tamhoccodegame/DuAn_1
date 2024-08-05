using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Mana : MonoBehaviour
{
    public Slider skillSlider;
    public float maxSkillMana = 100f;
    public float currentSkillMana;
    public float skillCost;
    // Start is called before the first frame update
    void Start()
    {
        currentSkillMana = maxSkillMana;
        skillSlider.maxValue = maxSkillMana;
        skillSlider.value = currentSkillMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseSkill(float cost)
    {
        if (currentSkillMana >= cost)
        {
            currentSkillMana -= cost;
            skillSlider.value = currentSkillMana;
            SetSkillMana(currentSkillMana);
        }
        else
        {
            Debug.Log("Not enough skill mana!");
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
