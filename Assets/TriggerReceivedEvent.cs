using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TriggerReceivedEvent : MonoBehaviour
{
	private Text text;
	private bool isDialoguing = false;
	private string lines;
	public Rune.RuneType runeType;
	Player player;
	private Inventory inventory;

	private void Start()
	{
		inventory = GameSession.instance.GetInventory();	
		text = GameSession.instance.GetDialogueText();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		player = collision.GetComponent<Player>();
		Debug.Log(collision.name);
		lines = "You received " + runeType.ToString();
		Debug.Log("Player entered trigger. Lines set to: " + lines);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if(isDialoguing) return;
			if(player != null)
			{
				Debug.Log("F key pressed and player is not null. Starting dialog.");
				isDialoguing = true;
				StartCoroutine(ShowDialog());
			}
			else
			{
				Debug.Log("F key pressed but player is null.");
			}
		}
	}

	IEnumerator ShowDialog()
	{
		text.gameObject.SetActive(true);
		text.transform.parent.gameObject.SetActive(true);

		text.text = string.Empty;
		foreach (char c in lines.ToCharArray())
		{
			text.text += c;
			yield return new WaitForSeconds(.08f);
		}
		yield return new WaitForSeconds(1f);

		text.gameObject.SetActive(false);
		text.transform.parent.gameObject.SetActive(false);
		inventory.AddRune(new Rune(runeType));
		isDialoguing = false;
	}
}
