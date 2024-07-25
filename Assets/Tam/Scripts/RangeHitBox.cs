using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHitBox : MonoBehaviour
{
    public int damage;
	private Player_Health playerHit;
	private void Start()
	{
		Destroy(gameObject, 2f);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerHit = collision.gameObject.GetComponent<Player_Health>();
		
		if (playerHit)
		{
			StartCoroutine(DestroySelf(collision));
		}
		
	}

	IEnumerator DestroySelf(Collider2D collision)
	{
		Vector3 direction = new Vector3(collision.transform.position.x - transform.position.x, 0, 0);
		direction.Normalize();

		playerHit.TakeDamage(damage);
		Knockback knockback = GetComponent<Knockback>();
		float destroyDelay = GetComponent<Knockback>().knockbackDuration;
		knockback.ApplyKnockback(collision.gameObject.transform, direction);

		yield return new WaitForSeconds(destroyDelay);
		Destroy(gameObject);
	}

	
}
