using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	public GameObject onFireEffect;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(!collision.GetComponent<Player_Health>()) return;
		Debug.Log(collision.name);
		var go = Instantiate(onFireEffect, collision.transform.position, Quaternion.identity, collision.transform);
	}
}
