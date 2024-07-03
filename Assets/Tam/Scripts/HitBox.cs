using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	private int damage;
	[SerializeField] private int playerDamage;
	[SerializeField] private int enemyDamage;
	private void Start()
	{
		if(playerDamage == 0)
		playerDamage = GetComponentInParent<PlayerController>()?.GetDamage() ?? playerDamage; 

		if(enemyDamage == 0)
		enemyDamage = GetComponentInParent<Enemy>()?.GetDamage() ?? enemyDamage;
	
		Debug.Log(enemyDamage);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
		Player_Health playerHit = collision.gameObject.GetComponent<Player_Health>();
		Vector3 direction = new Vector3(collision.transform.position.x - transform.position.x, 0, 0); 
		direction.Normalize();
		if (playerHit)
		{
			Debug.Log(enemyDamage);
			playerHit.TakeDamage(enemyDamage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
		if(enemyHit)
		{
			enemyHit.TakeDamage(playerDamage);
			Knockback knockback = GetComponent<Knockback>();
			knockback.ApplyKnockback(collision.gameObject.transform, direction);
		}
	}
}
