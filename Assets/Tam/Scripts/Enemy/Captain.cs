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
	[SerializeField] private ParticleSystem effectPrefabs;
	[SerializeField] private GameObject firePoint;
	[SerializeField] private GameObject effectPoint;
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
	//public override void Attack()
	//{
	//	base.Attack();

	//	if(player.transform.position.x - transform.position.x <= _attackRange)
	//	{
	//		StartCoroutine(CaptainSequence());
	//	}

		
	//}
	//public override IEnumerator AttackDelay()
	//{
	//	// Gọi lại phương thức AttackDelay của lớp cha để đảm bảo logic cơ bản được thực hiện
	//	yield return base.AttackDelay();
	//}

	//private IEnumerator CaptainSequence()
	//{
	//	yield return new WaitForSeconds(2f);

	//	// Kiểm tra khoảng cách giữa Captain và người chơi
	//	float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);
	//	if (distanceToPlayer <= attackRange + 5)
	//	{
	//		animator.SetTrigger("isAttack2");
	//		yield return new WaitForSeconds(2f); // Thêm thời gian chờ nếu cần thiết
	//	}

	//	// Gọi lại logic trong AttackDelay của lớp cha nếu cần
	//	if (distanceToPlayer > attackRange + 5)
	//	{
	//		ChangeState(State.Chase);
	//	}
	//}

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
