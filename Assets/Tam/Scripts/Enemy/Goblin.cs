using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _patrolSpeed;
	[SerializeField] private float _chaseSpeed;
	[SerializeField] private float _attackRange;
	[SerializeField] private float _chaseRange;
	[SerializeField] private int _damage;
	[SerializeField] private Transform[] _patrolPoints;
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

		isCoroutineRunning = true;
		StartCoroutine(AttackDelay());

	}

	public override IEnumerator AttackDelay()
	{
		yield return new WaitForSeconds(1f);
		animator.Play("Idle");
		yield return new WaitForSeconds(1f);
		animator.ResetTrigger("isAttack");
		if (Mathf.Abs(player.position.x - transform.position.x) > attackRange)
		{
			ChangeState(State.Chase);
		}

		isCoroutineRunning = false;
	}

}
