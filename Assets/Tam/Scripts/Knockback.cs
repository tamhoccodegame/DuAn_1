using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float knockbackForce = 10f;
	public float knockbackDuration = .2f;
	Vector3 vel;
	private PlayerController? playerController;
	private Enemy? enemy;

	public void ApplyKnockback(Transform target, Vector2 knockbackDirection)
	{
		Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
		playerController = target.GetComponent<PlayerController>();
		enemy = target.GetComponent<Enemy>();

		if (rb != null)
		{
			//vel = rb.velocity;
			if (playerController)
			{
				playerController.enabled = false;
			}
			else if(enemy) 
			{
				enemy.enabled = false;
			}

			rb.velocity = new Vector2(knockbackForce * knockbackDirection.x, knockbackForce * knockbackDirection.x);

			StartCoroutine(ResetVelocity(rb));
		}
	}
	private IEnumerator ResetVelocity(Rigidbody2D rb)
	{
		yield return new WaitForSeconds(knockbackDuration);
		if(playerController)
		playerController.enabled = true;
		if(enemy)
		enemy.enabled = true;
		//rb.velocity = vel;
	}
}
