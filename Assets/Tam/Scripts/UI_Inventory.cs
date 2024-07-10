using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Equipment equipment;

    private Transform runeSlotTemplate;
    private Transform runeSlotContainer;

    // Start is called before the first frame update
    void Start()
    {
        runeSlotTemplate = transform.Find("runeSlotTemplate");
        runeSlotContainer = transform.Find("runeSlotContainer");
    }

    public void SetInventory(Inventory _inventory)
    {
        inventory = _inventory;
		inventory.OnInventoryChange += Inventory_OnInventoryChange;
        RefreshInventory();
    }

	private void Inventory_OnInventoryChange(object sender, System.EventArgs e)
	{
        RefreshInventory();
	}

	public void RefreshInventory()
    {
        foreach(Transform child in runeSlotContainer)
        {
            if (child == runeSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;

        foreach(Rune rune in inventory.GetItemList())
        {
            RectTransform itemSlotRectTrasform = Instantiate(runeSlotTemplate,runeSlotContainer)
                                                .GetComponent<RectTransform>();
            itemSlotRectTrasform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //Equip

            };

            itemSlotRectTrasform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Drop
            };
            itemSlotRectTrasform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
            image.sprite = rune.GetSprite();

        }
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
