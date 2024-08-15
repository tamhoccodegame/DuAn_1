using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

	private void OnTriggerStay2D(Collider2D collision)
	{
		playerController.isGrounded = true;
		Debug.Log(collision.gameObject.name);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{ 
		playerController.isGrounded = false;
	}

	
}
