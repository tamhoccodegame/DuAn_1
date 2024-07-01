using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float knockbackForce = 10f;
	public float knockbackDuration = 0.2f;
	Vector3 vel;

	public void ApplyKnockback(Transform target, Vector2 knockbackDirection)
	{
		Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
		vel = rb.velocity;
		if (rb != null)
		{
			rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
			StartCoroutine(ResetVelocity(rb));
		}
	}
	private IEnumerator ResetVelocity(Rigidbody2D rb)
	{
		yield return new WaitForSeconds(knockbackDuration);
		rb.velocity = vel;
	}
}
