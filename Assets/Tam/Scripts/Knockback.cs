using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float knockbackForce;
	public float knockbackDuration;
	Vector3 vel;
	private PlayerController playerController;
	private Enemy enemy;

	public IEnumerator ApplyKnockback(Transform target, Vector2 knockbackDirection)
	{
		Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
		playerController = target.GetComponent<PlayerController>();
		enemy = target.GetComponent<Enemy>();

		if (rb != null)
		{
			////vel = rb.velocity;
			//if (playerController)
			//{
			//	playerController.enabled = false;
			//}
			//else if(enemy) 
			//{
			//	enemy.enabled = false;
			//}

			rb.velocity = new Vector2(knockbackForce * knockbackDirection.x, knockbackForce * Mathf.Abs(knockbackDirection.x));

			yield return null;
		}
	}

}
