using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
	public UI_Market uiMarket;
	public List<Item> items;

	void Start()
	{
		items = new List<Item>()
		{
			new (Item.ItemType.Helmet),
			new (Item.ItemType.Armor),
			new (Item.ItemType.Boot),
			new (Item.ItemType.Sword),
		};

		List<IShopItem> shopItems = new List<IShopItem>();
		foreach (Item item in items)
		{
			shopItems.Add(item);
		}
		uiMarket.RefreshShop(shopItems);
	}
}
