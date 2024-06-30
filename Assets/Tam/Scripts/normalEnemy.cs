using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemies : Enemy
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _patrolSpeed = 2f;
	[SerializeField] private float _chaseSpeed = 5f;
	[SerializeField] private float _attackRange = 2.5f;
	[SerializeField] private float _chaseRange = 15f;
	[SerializeField] private Transform[] _patrolPoints;
	[SerializeField] private GameObject _bulletPrefabs;
	[SerializeField] private ParticleSystem _effectPrefabs;
	[SerializeField] private GameObject _firePoint;
	[SerializeField] private GameObject _effectPoint;
	[SerializeField] private EnemyType _enemyType;


	// Start is called before the first frame update
	public override void Start()
    {
		base.Start();
		enemyType = _enemyType; 
		patrolSpeed = _patrolSpeed;
		chaseSpeed = _chaseSpeed;
		attackRange = _attackRange;
		chaseRange = _chaseRange;
        maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		patrolPoints = _patrolPoints;
		bulletPrefabs = _bulletPrefabs;
		effectPrefabs = _effectPrefabs;
		firePoint = _firePoint;
		effectPoint = _effectPoint;
    }

    // Update is called once per frame
	
	
	
}
