using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial_UI : MonoBehaviour
{
    public GameObject tutorialPanel; 
    public float activedTime = 5f;
    public GameObject craftingPanel_1;
    public GameObject craftingPanel_2;

    [HideInInspector]
    public GameObject buttonPanel;
    public GameObject runePanel;
    public int appearAmount;

    // Start is called before the first frame update
    void Start()
    {
        tutorialPanel.SetActive(true);
        StartCoroutine(HideTutorialPanelAfterDelay());
        buttonPanel.SetActive(false);
        runePanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//Mo menu len, trang dau tien hien len la rune in
        {
            buttonPanel.SetActive(true);
        }
    }



    private IEnumerator HideTutorialPanelAfterDelay()
    {
        yield return new WaitForSeconds(activedTime);
        tutorialPanel.SetActive(false);
    }

    public void Crafting_Panel()
    {
        if(appearAmount == 2)
        {
            craftingPanel_1.SetActive(false);
            craftingPanel_2.SetActive(false);
        }
    }

    public void AppearCount()
    {
        appearAmount++;
    }

}
