using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSlotUI : MonoBehaviour
{
    public GameObject[] lockedSlots;
    private Equipment equipment;
    void Start()
    {
		RefreshSlot();
	}

    public void SetEquipment(Equipment _equipment)
    {
        equipment = _equipment;
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
		RefreshSlot();
	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
        RefreshSlot();
	}

    private void RefreshSlot()
    {
		Debug.Log("Run this! Max slot: " + equipment.maxSlot);
		switch (equipment.maxSlot)
		{
			case 1:
				Destroy(lockedSlots[0].gameObject);
				break;
			case 2:
				Destroy(lockedSlots[1].gameObject);
				break;
			case 3:
				Destroy(lockedSlots[2].gameObject);
				break;
			case 4:
				Destroy(lockedSlots[3].gameObject);
				break;
		}
	}

	// Update is called once per frame
	void Update()
    {

    }
}
