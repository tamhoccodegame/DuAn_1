using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
	private static List<UI_Equipment> instance;

    private Equipment equipment;
	private Inventory inventory;
	
	private Transform runeEquipTemplate;
	private Transform runeEquipContainer;

	private EquipmentSlot[] equipmentSlots;
	// Start is called before the first frame update
	void Start()
    {
		instance = new List<UI_Equipment>();
		instance.Add(this);
		//UpdateSlotsVisual();
		runeEquipContainer = transform.Find("runeEquipContainer");
		runeEquipTemplate = runeEquipContainer.Find("runeEquipTemplate");
	}

    public void SetEquipment(Equipment _equipment)
    {
        equipment = _equipment;
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
		//equipment.OnSlotUnlocked += Equipment_OnSlotUnlocked;
        RefreshEquipment();
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
		Debug.Log("Set Inventory!");
		inventory = _inventory;
		inventory.OnInventoryChange += Inventory_OnInventoryChange;
		//RefreshEquipment();
	}

	private void Inventory_OnInventoryChange(object sender, System.EventArgs e)
	{
		//foreach (var item in instance)
		//	item.RefreshEquipment();
		RefreshEquipment();
	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		//foreach(var item in instance) 
		//item.RefreshEquipment();

		RefreshEquipment();
	}

	private void RefreshEquipment()
    {
		Debug.Log(gameObject.name);
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

			//itemSlotRectTrasform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
			Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
			image.sprite = rune.GetSprite();

			itemSlotRectTrasform.GetComponent<Button_UI>().ClickFunc = () =>
			{
				if(gameObject.name == "Equipped_Toolbar")
				{
					equipment.Unequip(rune);
					inventory.AddRune(rune);
				}
			};

		}
	}

	public class EquipmentSlot
	{
		private Image slot;
		private Sprite lockedSlotSprite;
		private Sprite unlockedSlotSprite;

		public EquipmentSlot(Image slot, Sprite lockedSlotSprite, Sprite unlockedSlotSprite)
		{
			this.slot = slot;
			this.lockedSlotSprite = lockedSlotSprite;
			this.unlockedSlotSprite = unlockedSlotSprite;
		}

		public void UpdateVisuals()
		{

		}
	}
}
