using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		float posX = transform.position.x;
		float posY = transform.position.y;
		string mapName = SceneManager.GetActiveScene().name;

		FindObjectOfType<GameSession>().UpdateCheckpoint(posX, posY, mapName);
	}
}

