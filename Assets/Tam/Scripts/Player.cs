using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Player : MonoBehaviour, IShopCustomer
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipment equipment;

    [SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private UI_Market[] uiMarkets;
	[SerializeField] private LockSlotUI uiLockSlotUI;
    [SerializeField] private UI_Equipment[] uiEquipments;

	public Sword sword;

    private PlayerController playerController;


	// Start is called before the first frame update
	void Awake()
    {

	}
	private void Start()
	{
		inventory = GameSession.instance.GetInventory();
		equipment = GameSession.instance.GetEquipment();

		uiInventory = GameSession.instance.GetUI_Inventory();
		uiMarkets = GameSession.instance.GetUI_Market();
		uiEquipments = GameSession.instance.GetUI_Equipment();
		uiLockSlotUI = GameSession.instance.GetLockSlotUI();


		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;

		sword.SetEquipment(equipment);

		uiInventory.SetInventory(inventory);
		uiInventory.SetEquipment(equipment);

		foreach (UI_Market uiMar in uiMarkets)
		{
			uiMar.transform.parent.gameObject.SetActive(false);
			uiMar.SetInventory(inventory);
		}

		uiLockSlotUI.SetEquipment(equipment);

		foreach (UI_Equipment uiEquip in uiEquipments)
		{
			uiEquip.SetInventory(inventory);
			uiEquip.SetEquipment(equipment);
		}

		playerController = GetComponent<PlayerController>();

	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		Debug.Log("Player EquipmentChange");
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
		int gold = GameSession.instance.GetCoin();
		return gold >= goldAmount;
	}

	public void BoughtItem(IShopItem shopItem)
	{
		Debug.Log("Bought" + shopItem.GetName());
	}
}
