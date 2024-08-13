using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string mapName;

	public int portalID;
	public int desPortalID;

	public Transform spawnPoint;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			GameSession.instance.desPortalID = desPortalID;
			GameSession.instance.LoadScene(mapName);
		}
	}

}
