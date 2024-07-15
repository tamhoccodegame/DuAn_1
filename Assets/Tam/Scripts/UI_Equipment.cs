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

	private EquipmentSlot[] equipmentSlots;
	// Start is called before the first frame update
	void Start()
    {
		equipmentSlots = new EquipmentSlot[4];
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
		RefreshEquipment();
	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		RefreshEquipment();
	}

	private void RefreshEquipment()
    {
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
				Debug.Log("Nhan input");
				equipment.Unequip(rune);
				inventory.AddRune(rune);
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
