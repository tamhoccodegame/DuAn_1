using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IShopItem
{
	public enum ItemType
	{
		Helmet,
		Armor,
		Boot,
		Sword,
	}

	public ItemType itemType;

	public Item(ItemType _itemType)
	{
		itemType = _itemType;
	}

	public Sprite GetSprite()
	{
		switch(itemType)
		{
			default:
			case ItemType.Helmet:			return GameAssets.instance.helmetSprite;
			case ItemType.Armor:			return GameAssets.instance.armorSprite;
			case ItemType.Boot:				return GameAssets.instance.bootSprite;
			case ItemType.Sword:		    return GameAssets.instance.swordSprite;
		}
	}

	public int GetCost()
	{
		switch(itemType)
		{
			default:
			case ItemType.Helmet: return 5000;
			case ItemType.Armor:  return 10000;
			case ItemType.Boot:	  return 2000;
			case ItemType.Sword:  return 15000;

		}
	}

	public string GetName()
	{
		return itemType.ToString();
	}
	public string GetDescription()
	{
		switch (itemType)
		{
			default:
			case ItemType.Helmet: return "Help you a litte more durable +50HP";
			case ItemType.Armor: return "Help you more durable +100HP";
			case ItemType.Sword: return "Help you slash a little stronger";
			case ItemType.Boot: return "Help you durable a bit +20HP";
		}
	}

}
