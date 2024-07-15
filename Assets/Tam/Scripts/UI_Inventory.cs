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
    private Transform runeDetail;
    private Transform runeSlotContainer;

    // Start is called before the first frame update
    void Awake()
    { 
        runeSlotContainer = transform.Find("runeSlotContainer");
        runeSlotTemplate = runeSlotContainer.Find("runeSlotTemplate");
        runeDetail = transform.Find("runeDetail");
        
        Debug.Log(runeSlotContainer.name);
        Debug.Log(runeSlotTemplate.name);
    }

    public void SetInventory(Inventory _inventory)
    {
        inventory = _inventory;
		inventory.OnInventoryChange += Inventory_OnInventoryChange;
        RefreshInventory();
    }
    public void SetEquipment(Equipment _equipment)
    {
        equipment = _equipment;
    }

	private void Inventory_OnInventoryChange(object sender, System.EventArgs e)
	{
        RefreshInventory();
	}

	public void RefreshInventory()
    {
        foreach (Transform child in runeSlotContainer)
        {
            if (child == runeSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Rune rune in inventory.GetItemList())
        {
            RectTransform itemSlotRectTrasform = Instantiate(runeSlotTemplate, runeSlotContainer)
                                                .GetComponent<RectTransform>();

            itemSlotRectTrasform.gameObject.SetActive(true);

            Debug.Log(itemSlotRectTrasform.position);

            itemSlotRectTrasform.GetComponent<Button_UI>().ClickFunc = () =>
            {
				//Equip
				//equipment.Equip(rune);
				//inventory.RemoveRune(rune);
                runeDetail.gameObject.SetActive(true);
				runeDetail.Find("Name").GetComponent<Text>().text = rune.runeType.ToString();
                
			};

            //itemSlotRectTrasform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            //{
            //    //Drop
            //};

            itemSlotRectTrasform.anchoredPosition = Vector2.zero;
            Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
            image.sprite = rune.GetSprite();

            
        }
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
