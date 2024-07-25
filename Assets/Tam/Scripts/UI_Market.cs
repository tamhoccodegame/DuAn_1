using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Market : MonoBehaviour
{
    private Transform marketSlotTemplate;
    private Transform marketSlotContainer;
    
    void Awake()
    {
        marketSlotContainer = transform.Find("marketSlotContainer");
        marketSlotTemplate = marketSlotContainer.Find("marketSlotTemplate");
    }

	private void Start()
	{
		
	}

    //Need Create an Interface UI_Market. And UI_RuneMarket and UI_ItemMarket is inherit this Interface
    public void CreateItemButton(Rune.RuneType runType, Sprite runeSprite, int runeCost)
    {
        RectTransform marketSlotRectTransform = Instantiate(marketSlotTemplate, marketSlotContainer).GetComponent<RectTransform>();
        Image image = marketSlotRectTransform.Find("Image").GetComponent<Image>();
        image.sprite = runeSprite;
        marketSlotRectTransform.Find("Cost").GetComponent<Text>().text = runeCost.ToString();
    }


}
