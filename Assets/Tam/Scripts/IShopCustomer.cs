using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
	bool TryToSpendGold(int goldAmount);
	void BoughtItem(IShopItem shopItem);
}
