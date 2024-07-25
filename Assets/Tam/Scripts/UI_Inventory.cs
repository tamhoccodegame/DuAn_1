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
    private Transform equipButton;
    private Transform runeSlotContainer;

    private static Transform lastHoverSlot;

    // Start is called before the first frame update
    void Awake()
    { 
        runeSlotContainer = transform.Find("runeSlotContainer");
        runeSlotTemplate = runeSlotContainer.Find("runeSlotTemplate");
        runeDetail = transform.Find("runeDetail");
        equipButton = transform.Find("EquipButton");
        lastHoverSlot = null;
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
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
        //RefreshInventory();
    }

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		RefreshInventory();
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

			itemSlotRectTrasform.anchoredPosition = Vector2.zero;
			itemSlotRectTrasform.Find("Hover").gameObject.SetActive(false);
			Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
			image.sprite = rune.GetSprite();

            //Equip
			itemSlotRectTrasform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if(lastHoverSlot != null) lastHoverSlot.gameObject.SetActive(false);
                itemSlotRectTrasform.Find("Hover").gameObject.SetActive(true);
                lastHoverSlot = itemSlotRectTrasform.Find("Hover");
                runeDetail.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(true);
				runeDetail.Find("Name").GetComponent<Text>().text = rune.runeType.ToString();
                runeDetail.Find("Image").GetComponent<Image>().sprite = rune.GetSprite();
				equipButton.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    if(equipment.CanEquip())
                    {
						equipment.Equip(rune);
                        inventory.RemoveRune(rune);
                        runeDetail.gameObject.SetActive(false);
                        equipButton.gameObject.SetActive(false);
					}
                  
                };
			};

           

            //itemSlotRectTrasform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            //{
            //    //Drop
            //};

           

            
        }
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
