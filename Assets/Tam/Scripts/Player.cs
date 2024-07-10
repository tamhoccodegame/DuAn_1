using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    private Equipment equipment;

    public UI_Inventory uiInventory;
    public UI_Equipment uiEquipment;

    // Start is called before the first frame update
    void Awake()
    {
        inventory = new Inventory();
        equipment = new Equipment();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
