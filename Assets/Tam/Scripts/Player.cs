using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    private Equipment equipment;

    public UI_Inventory uiInventory;
    private UI_Equipment[] uiEquipments;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        equipment = new Equipment();
		uiEquipments = FindObjectsByType<UI_Equipment>(FindObjectsSortMode.None);

		playerController = GetComponent<PlayerController>();
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;

		inventory.AddRune(new Rune(Rune.RuneType.Damage));
		inventory.AddRune(new Rune(Rune.RuneType.DoubleJump));

		uiInventory.SetInventory(inventory);
		uiInventory.SetEquipment(equipment);

		foreach(var uiEquip in uiEquipments)
		{
			uiEquip.SetInventory(inventory);
			uiEquip.SetEquipment(equipment);
		}

	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		foreach (Rune rune in equipment.GetEquipmentList())
		{
			switch (rune.runeType)
			{ 
				case Rune.RuneType.Damage:
					playerController.SetDamage(15);
					break;
				case Rune.RuneType.AttackSpeed:
					break;
				case Rune.RuneType.DoubleJump:
					playerController.canDoubleJump = true;
					break;
				case Rune.RuneType.Dash:
					break;
			}
		}
	}

    private void RefreshPlayerStat()
    {
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
