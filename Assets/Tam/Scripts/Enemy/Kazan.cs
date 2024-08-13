using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kazan : Enemy
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _patrolSpeed;
	[SerializeField] private float _chaseSpeed;
	[SerializeField] private float _attackRange;
	[SerializeField] private float _chaseRange;
	[SerializeField] private int _damage;
	[SerializeField] private Transform[] _patrolPoints;
	[SerializeField] private GameObject bulletPrefabs;
	[SerializeField] private GameObject firePoint;
	[SerializeField] private EnemyType _enemyType;

	// Start is called before the first frame update
	public override void Awake()
	{
		base.Awake();
		enemyType = _enemyType;
		patrolSpeed = _patrolSpeed;
		chaseSpeed = _chaseSpeed;
		attackRange = _attackRange;
		chaseRange = _chaseRange;
		damage = _damage;
		maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		patrolPoints = _patrolPoints;
	}


	// Update is called once per frame
	public override void Attack()
	{
		if (isCoroutineRunning) return;

		rb.velocity = Vector2.zero;

		isCoroutineRunning = true;
		StartCoroutine(AttackDelay());

	}

	public override IEnumerator AttackDelay()
	{
		FireBall();
		yield return new WaitForSeconds(3f);

		if (Mathf.Abs(player.position.x - transform.position.x) > attackRange)
		{
			ChangeState(State.Chase);
		}

		isCoroutineRunning = false;
	}

	public void FireBall()
	{
		var spawnedBullet = Instantiate(bulletPrefabs, firePoint.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
		spawnedBullet.transform.localScale = new Vector3(direction.x * spawnedBullet.transform.localScale.x,
															spawnedBullet.transform.localScale.z,
															 spawnedBullet.transform.localScale.z);
		spawnedBullet.AddForce(new Vector2(direction.x * 12f, 0), ForceMode2D.Impulse);
		Destroy(spawnedBullet.gameObject, 4f);
	}
}
