using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	private int damage;
	[SerializeField] private int playerDamage;
	[SerializeField] private int enemyDamage;
	[SerializeField] private bool canDestroySelf = false;
	private void Start()
	{
		if(playerDamage == 0)
		playerDamage = GetComponentInParent<PlayerController>()?.GetDamage() ?? playerDamage; 

		if(enemyDamage == 0)
		enemyDamage = GetComponentInParent<Enemy>()?.GetDamage() ?? enemyDamage;
	}

	public int GetDamage()
	{
		return damage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.gameObject.layer != LayerMask.NameToLayer("Player"))
		&& (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))) return;

		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && canDestroySelf)
		{
			Destroy(this.gameObject);
			return;
		}
		Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
		Player_Health playerHit = collision.gameObject.GetComponent<Player_Health>();
		Vector3 direction = new Vector3(collision.transform.position.x - transform.position.x, 0, 0); 
		direction.Normalize();

		if (playerHit)
		{
			StartCoroutine(HandlePlayerHit(playerHit, collision.gameObject.transform, direction));
		}
		if(enemyHit)
		{
			StartCoroutine(HandleEnemyrHit(enemyHit, collision.gameObject.transform, direction));	
		}
	}

	private IEnumerator HandlePlayerHit(Player_Health playerHit, Transform target, Vector3 direction)
	{
		Knockback knockback = GetComponent<Knockback>();
		
		yield return StartCoroutine(playerHit.TakeDamage(enemyDamage));

		if (knockback != null)
		{
			yield return StartCoroutine(knockback.ApplyKnockback(target, direction));
		}

	}

	private IEnumerator HandleEnemyrHit(Enemy enemyHit, Transform target, Vector3 direction)
	{
		Knockback knockback = GetComponent<Knockback>();

		enemyHit.TakeDamage(playerDamage);

		if (knockback != null)
		{
			yield return StartCoroutine(knockback.ApplyKnockback(target, direction));
		}
	}
}
