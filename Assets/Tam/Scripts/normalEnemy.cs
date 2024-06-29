using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemies : Enemy
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _speed = 2f;

	private Transform player;
	private Rigidbody2D rb;

	// Start is called before the first frame update
	public override void Start()
    {
		player = GameObject.Find("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
        maxHealth = _maxHealth;
		currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

	private void Chase()
	{
		Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();
		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

	}

	
}
