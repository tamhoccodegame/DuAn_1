using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadzone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{

		Player_Health player = collision.GetComponent<Player_Health>();
		if (player)
		{
			StartCoroutine(player.TakeDamage(9999));
		}
	}
}
