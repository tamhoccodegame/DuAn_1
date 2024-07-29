using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerUIMarket : MonoBehaviour
{
	[SerializeField] private UI_Market uiMarket;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();
		if(shopCustomer != null)
		{
			uiMarket.Show(shopCustomer);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		uiMarket.Hide();
	}

}
