using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveItem : MonoBehaviour
{
    public Text text;
    private Inventory inventory;
    public Rune.RuneType Rune;
	// Start is called before the first frame update

	private void Start()
	{
		inventory = GameSession.instance.GetInventory();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			StartCoroutine(AddRuneToInventory());
		}
	}

	private IEnumerator AddRuneToInventory()
	{
		inventory.AddRune(new Rune(Rune));
		text.text = "You Received Rune " + Rune;
		text.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		text.gameObject.SetActive(false);
	}
}
