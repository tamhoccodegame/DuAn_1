using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player player = collision.GetComponent<Player>();
		if (player)
		{
			float posX = player.gameObject.transform.position.x;
			float posY = player.gameObject.transform.position.y;
			string mapName = SceneManager.GetActiveScene().name;
			GameSession.instance.UpdateCheckpoint(posX, posY, mapName);
			foreach(Transform child in transform)
			{
				child.gameObject.SetActive(true);
			}
		}
	}
}
