using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	private int damage;
	int playerDamage;
	int enemyDamage;

	private void Start()
	{
		//playerDamage = 
		enemyDamage = GetComponentInParent<Enemy>().GetDamage();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
		Player_Health playerHit = collision.gameObject.GetComponent<Player_Health>();
		Vector3 direction = new Vector3(collision.transform.position.x - transform.position.x, 0, 0); 
		direction.Normalize();
		if (playerHit)
		{
			playerHit.TakeDamage(enemyDamage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
		else
		{
			//enemyHit.TakeDamage(enemyDamage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
	}
}
