using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    private Equipment equipment;

    public UI_Inventory uiInventory;
    public UI_Equipment uiEquipment;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        equipment = new Equipment();

        inventory.AddRune(new Rune(Rune.RuneType.Damage));
        inventory.AddRune(new Rune(Rune.RuneType.AttackSpeed));

        uiInventory.SetInventory(inventory);
		uiInventory.SetEquipment(equipment);

		uiEquipment.SetEquipment(equipment);

		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;

    }

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		RefreshPlayerStat();
	}

    private void RefreshPlayerStat()
    {
        foreach (Rune rune in equipment.GetEquipmentList())
        {
            switch (rune.runeType)
            {
                case Rune.RuneType.Damage:
                    break;
                case Rune.RuneType.AttackSpeed:
                    break;
                case Rune.RuneType.DoubleJump:
                    break;
                case Rune.RuneType.Speed:
                    break;
            }
        }
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
