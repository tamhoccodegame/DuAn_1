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
        }
        else
        {
            Debug.Log("Not enough skill mana!");
        }
    }
}
