using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSlotUI : MonoBehaviour
{
    public GameObject[] lockedSlots;
    private Equipment equipment;
    void Start()
    {
		//RefreshSlot();
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
		Debug.Log(equipment.maxSlot);
		for(int i = 0; i < equipment.maxSlot; i++)
		{
			if(lockedSlots[i] != null) 
			Destroy(lockedSlots[i]);
		}
	}

	// Update is called once per frame
	void Update()
    {

    }
}
