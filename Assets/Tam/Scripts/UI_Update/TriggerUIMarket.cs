using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TriggerUIMarket : MonoBehaviour
{
	[SerializeField] private UI_Market uiMarket;
	[SerializeField] private IShopCustomer shopCustomer;
	public string lines;
	private int lineIndex = 0;
	private Text text;
	private bool isDialoguing = false;


	private void Start()
	{
		if(gameObject.name == "TriggerShopRune")
		{
			uiMarket = GameSession.instance.GetUI_Market()[0];
		}
		else if(gameObject.name == "TriggerShopItem")
		{
			uiMarket = GameSession.instance.GetUI_Market()[1];
		}
		text = GameSession.instance.GetDialogueText();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<Player>())
		shopCustomer = collision.GetComponent<IShopCustomer>();
	}

	private void Update()
	{
		if (shopCustomer != null)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				StartCoroutine(ShowShopUI());
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				uiMarket.Hide();
			}
		}
	}

	IEnumerator ShowShopUI()
	{
		if (!isDialoguing)
		{
			text.gameObject.SetActive(true);
			text.transform.parent.gameObject.SetActive(true);
			isDialoguing = true;
			yield return StartCoroutine(Diaglogue());
			isDialoguing = false;
		}
		if (isDialoguing) yield break;
		if (shopCustomer != null)
		{
			uiMarket.Show(shopCustomer);
		}
		else
		{
			Debug.LogError("Can't find Player");
		}
	}

	IEnumerator Diaglogue()
	{
		text.text = string.Empty;
		foreach(char c in lines.ToCharArray())
		{
			text.text += c;
			yield return new WaitForSeconds(.08f);
		}
		yield return new WaitForSeconds(1f);
		text.gameObject.SetActive(false);
		text.transform.parent.gameObject.SetActive(false);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		uiMarket.Hide();
		shopCustomer = null;
	}

}
