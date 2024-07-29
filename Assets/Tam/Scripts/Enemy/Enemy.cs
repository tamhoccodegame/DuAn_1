using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform player;

    protected float maxHealth;
    [SerializeField] protected float currentHealth;
    protected float patrolSpeed;
    protected float chaseSpeed;
    protected float attackRange;
    protected float chaseRange;
    protected int damage;


    protected Transform[] patrolPoints;
    private int currentPatrolIndex;
    protected bool isCoroutineRunning = false;
    protected bool isAlive = true;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected Vector3 direction;

    [SerializeField] private ParticleSystem vialityEffect;

    public enum State
    {
        Patrol,
        Attack,
        Chase,
		Hurting,
	}

    public enum EnemyType
    {
        Melee,
        Range,
    }

    protected State currentState;
    protected EnemyType enemyType;

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
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
    }

    // Update is called once per frame  
    public virtual void Update()
    {
        if (!isAlive) return;
        if (isCoroutineRunning) return;
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

    protected void ChangeState(State newState)
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

    protected void LookAtDirection(Vector3 direction)
    {
        transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x),
                                          transform.localScale.y, transform.localScale.z);
    }

    public virtual void Chase()
    {
        direction = new Vector3(player.position.x - transform.position.x, 0, 0);
        direction.Normalize();

        LookAtDirection(direction);

        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        if (Mathf.Abs(player.position.x - transform.position.x) <= attackRange)
        {
            ChangeState(State.Attack);
        }
        //Debug.Log("Direction: " + direction + " | Velocity: " + rb.velocity);
    }

 

	public virtual void Attack()
	{
       	
    }

    
    private void Hurting()
    {
        rb.velocity = Vector2.zero;
    }

    //private void SpawnEffect()
    //{
    //    Instantiate(effectPrefabs, effectPoint.transform.position, transform.rotation);
    //}

    public virtual IEnumerator AttackDelay()
    {
        return null;
	}


	public void TakeDamage(float damage)
    {
        if (!isAlive) return;

		rb.velocity = Vector2.zero;
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
			isAlive = false;
            return;
		}

		animator.SetTrigger("isHurt");
    }

	private void Die()
    {
        this.enabled = false;
        animator.SetBool("isDead", true);
        var go = Instantiate(vialityEffect, transform);
		//Quaternion newRotation = Quaternion.Euler(-90, 0, 0);
  //      go.transform.rotation = newRotation;
        StartCoroutine(DieDelay());
    }

    private IEnumerator DieDelay()
    {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), gameObject.layer, true);
		yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
