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


	// Start is called before the first frame update
	public override void Start()
    {
		base.Start();
		patrolSpeed = _patrolSpeed;
		chaseSpeed = _chaseSpeed;
		attackRange = _attackRange;
		chaseRange = _chaseRange;
        maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		patrolPoints = _patrolPoints;
    }

    // Update is called once per frame
	
	
	
}
