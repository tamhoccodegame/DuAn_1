using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
    private Equipment equipment;

	private Transform runeSlotTemplate;
	private Transform runeSlotContainer;
	// Start is called before the first frame update
	void Start()
    {
		runeSlotTemplate = transform.Find("runeSlotTemplate");
		runeSlotContainer = transform.Find("runeSlotContainer");
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
		foreach (Transform child in runeSlotContainer)
		{
			if (child == runeSlotTemplate) continue;
			Destroy(child.gameObject);
		}

		int x = 0;
		int y = 0;
		float itemSlotCellSize = 75f;

		foreach (Rune rune in equipment.GetEquipmentList())
		{
			RectTransform itemSlotRectTrasform = Instantiate(runeSlotTemplate, runeSlotContainer)
												.GetComponent<RectTransform>();

			itemSlotRectTrasform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
			Image image = itemSlotRectTrasform.Find("Image").GetComponent<Image>();
			image.sprite = rune.GetSprite();

		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
