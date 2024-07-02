using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform player;

    protected float maxHealth;
    protected float currentHealth;
    protected float patrolSpeed;
    protected float chaseSpeed;
    protected float attackRange;
    protected float chaseRange;
    protected int damage;


    protected Transform[] patrolPoints;
    protected GameObject? bulletPrefabs;
    protected ParticleSystem? effectPrefabs;
    protected GameObject firePoint;
    protected GameObject effectPoint;
    private int currentPatrolIndex;
    private bool isCoroutineRunning = false;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected Vector3 direction;

    public enum State
    {
        Patrol,
        Attack,
        Chase,
    }

    public enum EnemyType
    {
        Melee,
        Range,
    }

    public State currentState;
    public EnemyType enemyType;

    public int GetDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    public virtual void Awake()
    {
        currentState = State.Patrol;
        currentPatrolIndex = 0;
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    
    private void Patrol()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) <= chaseRange)
        {
            ChangeState(State.Chase);
        }

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        direction = new Vector3(targetPoint.position.x - transform.position.x, 0, 0);
        direction.Normalize();
        LookAtDirection(direction);

        rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);

        if (Vector2.Distance(transform.position, targetPoint.position) <= 4f)
        {
            rb.velocity = Vector2.zero;
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

    }

    private void LookAtDirection(Vector3 direction)
    {
        transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x),
                                          transform.localScale.y, transform.localScale.z);
    }

    private void Chase()
    {
        direction = new Vector3(player.position.x - transform.position.x, 0, 0);
        direction.Normalize();

        LookAtDirection(direction);

        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        if (Mathf.Abs(player.position.x - transform.position.x) <= attackRange)
        {
            ChangeState(State.Attack);
        }
        if (Mathf.Abs(player.position.x - transform.position.x) > chaseRange)
        {
            ChangeState(State.Patrol);
        }
        //Debug.Log("Direction: " + direction + " | Velocity: " + rb.velocity);
    }

    private void Attack()
	{
		animator.SetTrigger("isAttack");

		if (isCoroutineRunning) return;

        direction = direction = new Vector3(player.position.x - transform.position.x, 0, 0);
        direction.Normalize();

		LookAtDirection(direction); 
        rb.velocity = Vector2.zero;
        if (enemyType == EnemyType.Range)
        {
            var spawnedBullet = Instantiate(bulletPrefabs, firePoint.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            spawnedBullet.transform.localScale = new Vector3 (direction.x * spawnedBullet.transform.localScale.x, 
                                                                spawnedBullet.transform.localScale.z, 
                                                                 spawnedBullet.transform.localScale.z);
            spawnedBullet.velocity = new Vector2(direction.x * 5, 0);
        }

		StartCoroutine(AttackDelay());
		
    }

    private void SpawnEffect()
    {
        Instantiate(effectPrefabs, effectPoint.transform.position, transform.rotation);
    }

    private IEnumerator AttackDelay()
    {
        isCoroutineRunning = true;
        animator.ResetTrigger("isAttack");
        yield return new WaitForSeconds(2f);
		if (Mathf.Abs(player.position.x - transform.position.x) > attackRange)
		{
			ChangeState(State.Chase);
		}
		isCoroutineRunning = false;
	}


	public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.enabled = false;
        animator.SetBool("isDead", true);
        Destroy(gameObject);
    }


}
