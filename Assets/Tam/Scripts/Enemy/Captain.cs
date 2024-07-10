using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy
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

		animator.SetTrigger("isAttack");
		rb.velocity = Vector2.zero;


		direction = direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();

		LookAtDirection(direction);

		isCoroutineRunning = true;
		StartCoroutine(AttackDelay());
	}

	public override IEnumerator AttackDelay()
	{
		yield return new WaitForSeconds(.3f);
		animator.ResetTrigger("isAttack");
		yield return new WaitForSeconds(1f);
		while(Mathf.Abs(player.position.x - transform.position.x) <= attackRange + 5)
		{
			animator.SetTrigger("isAttack2");
			yield return new WaitForSeconds(.5f);
			animator.ResetTrigger("isAttack2");
			yield return new WaitForSeconds(1f);
		}
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
		spawnedBullet.velocity = new Vector2(direction.x * 5, 0);
		spawnedBullet.GetComponent<RangeHitBox>().damage = damage;
	}

}
