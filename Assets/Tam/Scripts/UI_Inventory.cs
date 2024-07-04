using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Equipment equipment;

    private Transform runeSlotTemplate;
    private Transform runeSlotContainer;

    // Start is called before the first frame update
    void Start()
    {
        runeSlotTemplate = transform.Find("runeSlotTemplate");
        runeSlotContainer = transform.Find("runeSlotContainer");
    }

    public void SetInventory(Inventory _inventory)
    {
        inventory = _inventory;
        RefershInventory();
    }

    public void RefershInventory()
    {
        foreach(Transform child in runeSlotContainer)
        {
            if (child == runeSlotTemplate) continue;
            Destroy(child.gameObject);
        }
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
