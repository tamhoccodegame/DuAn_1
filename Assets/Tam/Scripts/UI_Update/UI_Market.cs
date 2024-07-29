using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

public class UI_Market : MonoBehaviour
{
    private Transform marketSlotTemplate;
    private Transform marketSlotContainer;

	private static Transform lastHoverSlot;
	private Transform marketDetail;
	private Transform buyButton;

	private Inventory inventory;

	private IShopCustomer shopCustomer;

	void Awake()
    {
        marketSlotContainer = transform.Find("marketSlotContainer");
        marketSlotTemplate = marketSlotContainer.Find("marketSlotTemplate");
		marketDetail = transform.Find("marketDetail");
		buyButton = transform.Find("buyButton");
		lastHoverSlot = null;
	}

	private void Start()
	{
		
	}

    public void SetInventory(Inventory _inventory)
	{
		inventory = _inventory;
	}

    public void RefreshShop(List<IShopItem> shopItems)
    {
        foreach(Transform child in marketSlotContainer)
        {
            if (child == marketSlotTemplate)
                continue;
            Destroy(child.gameObject);
        }

        foreach(IShopItem shopItem in shopItems)
        {
			RectTransform marketSlotRectTransform = Instantiate(marketSlotTemplate, marketSlotContainer).GetComponent<RectTransform>();
            marketSlotRectTransform.gameObject.SetActive(true);
			marketSlotRectTransform.Find("Hover").gameObject.SetActive(false);

			Image image = marketSlotRectTransform.Find("Image").GetComponent<Image>();
			image.sprite = shopItem.GetSprite();
			marketSlotRectTransform.Find("Cost").GetComponent<Text>().text = shopItem.GetCost().ToString();

            marketSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
				if (lastHoverSlot != null) lastHoverSlot.gameObject.SetActive(false);
				marketSlotRectTransform.Find("Hover").gameObject.SetActive(true);
				lastHoverSlot = marketSlotRectTransform.Find("Hover");

				StartCoroutine(ShowDetail(shopItem));

				buyButton.GetComponent<Button_UI>().ClickFunc = () =>
				{
					if (!shopCustomer.TryToSpendGold(shopItem.GetCost())) return;
					shopCustomer.BoughtItem(shopItem);
					//If the item is Rune
					if(shopItem is Rune)
					{
						Rune rune = shopItem as Rune;
						inventory.AddRune(rune);

						shopItems.Remove(shopItem);
						RefreshShop(shopItems);
					}

					else if (shopItem is Item)
					{
						shopItems.Remove(shopItem);
						RefreshShop(shopItems);

						switch (shopItem.GetName())
						{
							default:
							case "Armor":
								FindObjectOfType<Player_Health>().maxHealth += 20;
								break;
							case "Helmet":
								FindObjectOfType<Player_Health>().maxHealth += 10;
								break;
						}
					}
				};
			};
		}
       
    }

	IEnumerator ShowDetail(IShopItem shopItem)
	{
		marketDetail.gameObject.SetActive(true);
		marketDetail.Find("Describe").GetComponent<Text>().text = shopItem.GetDescription();
		yield return new WaitForSeconds(5f);
		marketDetail.gameObject.SetActive(false);
	}

	public void Show(IShopCustomer _shopCustomer)
	{
		shopCustomer = _shopCustomer;
		transform.parent.gameObject.SetActive(true);
	}

	public void Hide()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
