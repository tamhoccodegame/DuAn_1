using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{

    private Equipment equipment;
	private Inventory inventory;
	
	private Transform runeEquipTemplate;
	private Transform runeEquipContainer;

	// Start is called before the first frame update
	void Awake()
    {
		runeEquipContainer = transform.Find("runeEquipContainer");
		runeEquipTemplate = runeEquipContainer.Find("runeEquipTemplate");
	}

    public void SetEquipment(Equipment _equipment)
    {
		equipment = null;
		equipment = _equipment;
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
        RefreshEquipment();
    }

	public void Unsubscribe()
	{
		equipment.OnEquipmentChange -= Equipment_OnEquipmentChange;
	}

	//private void Equipment_OnSlotUnlocked(object sender, System.EventArgs e)
	//{
	//	UpdateSlotsVisual();
	//}

	//private void UpdateSlotsVisual()
	//{
	//	foreach (EquipmentSlot slot in equipmentSlots)
	//	{
	//		slot.UpdateVisuals();
	//	}
	//}

	public void SetInventory(Inventory _inventory)
	{
		inventory = _inventory;
		//inventory.OnInventoryChange += Inventory_OnInventoryChange;
		//RefreshEquipment();
	}

	//private void Inventory_OnInventoryChange(object sender, System.EventArgs e)
	//{
	//	//foreach (var item in instance)
	//	//	item.RefreshEquipment();
	//	RefreshEquipment();
	//}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		RefreshEquipment();
	}

	private void RefreshEquipment()
    {
		if (runeEquipContainer == null)
		{
			Debug.LogError("runeSlotContainer is null");
			return;
		}

		foreach (Transform child in runeEquipContainer)
		{
			if (child == runeEquipTemplate) continue;
			Destroy(child.gameObject);
		}

		foreach (Rune rune in equipment.GetEquipmentList())
		{
			RectTransform itemSlotRectTrasform = Instantiate(runeEquipTemplate, runeEquipContainer)
												.GetComponent<RectTransform>();
			itemSlotRectTrasform.gameObject.SetActive(true);

			Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
			image.sprite = rune.GetSprite();

			itemSlotRectTrasform.GetComponent<Button_UI>().ClickFunc = () =>
			{
				if(gameObject.name == "Equipped_Toolbar") //Chi trong inventory moi cho thao
				{
					equipment.Unequip(rune);
					inventory.AddRune(rune);
				}
			};

		}
	}
}
