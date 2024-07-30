using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour, IShopCustomer
{
    private Inventory inventory;
    private Equipment equipment;

    public UI_Inventory uiInventory;
	private UI_Market[] uiMarkets;
	public LockSlotUI uiLockSlotUI;
    private UI_Equipment[] uiEquipments;

	public Sword sword;

    private PlayerController playerController;
	
    // Start is called before the first frame update
    void Awake()
    {
        inventory = new Inventory();
		inventory.AddRune(new Rune(Rune.RuneType.Damage));
		inventory.AddRune(new Rune(Rune.RuneType.DoubleJump));
		inventory.AddRune(new Rune(Rune.RuneType.Fire));

		equipment = new Equipment();
		

		playerController = GetComponent<PlayerController>();
		
	}
	private void Start()
	{
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;

		uiEquipments = FindObjectsByType<UI_Equipment>(FindObjectsSortMode.None);
		uiMarkets = FindObjectsByType<UI_Market>(FindObjectsSortMode.None);

		sword.SetEquipment(equipment);

		uiInventory.SetInventory(inventory);
		uiInventory.SetEquipment(equipment);

		foreach(var uiMar in uiMarkets)
		{
			uiMar.transform.parent.gameObject.SetActive(false);
			uiMar.SetInventory(inventory);
		}

		uiLockSlotUI.SetEquipment(equipment);

		foreach (var uiEquip in uiEquipments)
		{
			uiEquip.SetInventory(inventory);
			uiEquip.SetEquipment(equipment);
		}
		
	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		//ResetPlayerStat first
		playerController.SetDamage(-50);
		playerController.canDoubleJump = false;
		playerController.canDash = false;

		foreach (Rune rune in equipment.GetEquipmentList())
		{
			switch (rune.runeType)
			{ 
				case Rune.RuneType.Damage:
					playerController.SetDamage(50);
					break;
				case Rune.RuneType.DoubleJump:
					playerController.canDoubleJump = true;
					break;
				case Rune.RuneType.Dash:
					playerController.canDash = true;
					break;
				case Rune.RuneType.Fire:
					break;
			
			}
		}
	}

    public void RefreshPlayerStat()
    {
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }

	public bool TryToSpendGold(int goldAmount)
	{
		int gold = 10000;
		return gold >= goldAmount;
	}

	public void BoughtItem(IShopItem shopItem)
	{
		Debug.Log("Bought" + shopItem.GetName());
	}
}
