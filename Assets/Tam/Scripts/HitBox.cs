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
		Debug.Log(playerHit);
		if (playerHit)
		{
			playerHit.TakeDamage(enemyDamage);
			GetComponent<Knockback>().ApplyKnockback(playerHit.transform, (playerHit.transform.position - transform.position).normalized);
		}
	}
}
