using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHitBox : MonoBehaviour
{
    public int damage;

	private void Start()
	{
		Destroy(gameObject, 2f);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
		Player_Health playerHit = collision.gameObject.GetComponent<Player_Health>();
		Vector3 direction = new Vector3(collision.transform.position.x - transform.position.x, 0, 0);
		direction.Normalize();
		if (playerHit)
		{
			Debug.Log(damage);
			playerHit.TakeDamage(damage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
		if (enemyHit)
		{
			enemyHit.TakeDamage(damage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
		float destroyDelay = GetComponent<Knockback>().knockbackDuration + .3f;
		//StartCoroutine(DestroySelf(destroyDelay));
		
	}

	
}
