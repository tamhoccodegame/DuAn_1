using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
    private Equipment equipment;
	
	private Transform runeEquipTemplate;
	private Transform runeEquipContainer;
	// Start is called before the first frame update
	void Start()
    {
		runeEquipContainer = transform.Find("runeEquipContainer");
		runeEquipTemplate = runeEquipContainer.Find("runeEquipTemplate");
	}

    public void SetEquipment(Equipment _equipment)
    {
        equipment = _equipment;
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
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

		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
