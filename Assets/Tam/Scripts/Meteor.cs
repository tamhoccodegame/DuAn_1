using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	public GameObject explosionPrefab;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Vector2 impactPoint = collision.ClosestPoint(transform.position);

			var go = Instantiate(explosionPrefab, impactPoint, Quaternion.identity);
			Destroy(go, 1.5f);	
			Destroy(gameObject, 1.6f);
		}
	}
}
