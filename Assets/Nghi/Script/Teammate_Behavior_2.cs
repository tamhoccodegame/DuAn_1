using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Teammate_Behavior_2 : MonoBehaviour
{
    public float followDistance = 2f;
    //public float chaseRange = 1f; // Khoảng cách gần hơn trước khi bắt đầu tấn công*****
    public float moveSpeed = 9f;

    public Transform attackPoint;//*
    public float attackRange = 5f;
    public float attackInterval = 1f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;
    public int maxHealth = 100;
    public string[] attackCombos; // Array chứa các tên trigger của các combo tấn công

    private Transform player;
    private Transform target;
    private float attackTimer;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;

    private bool facingRight = true; // Biến để theo dõi hướng hiện tại

    private enum State
    {
        Idle,
        FollowPlayer,
        ChaseEnemy,
        AttackEnemy
    }

    private State currentState;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Nếu không cần trọng lực
        currentState = State.FollowPlayer; // Bắt đầu ở trạng thái FollowPlayer
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.FollowPlayer:
                FollowPlayer();
                break;
            case State.ChaseEnemy:
                ChaseTarget();
                break;
            case State.AttackEnemy:
                AttackTarget();
                break;
        }

        if (currentState != State.AttackEnemy && currentState != State.ChaseEnemy)
        {
            FindTarget();

        }
    }

    void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        if (enemies.Length > 0)
        { 
            target = enemies[0].transform; // Tìm kẻ thù gần nhất
            currentState = State.ChaseEnemy;
        }
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > followDistance)
        {
            animator.SetBool("isRunning", true);
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            FlipCharacter(direction.x);//***
        }
        else
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero; // Ngừng di chuyển khi đến gần player
        }
    }

    void ChaseTarget()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)//(distanceToTarget > attackRange)
            {
                animator.SetBool("isRunning", true);
                Vector2 direction = (target.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
                FlipCharacter(direction.x);//***
            }
            else
            {
                animator.SetBool("isRunning", false);
                currentState = State.AttackEnemy;
            }
        }
        else
        {
            currentState = State.FollowPlayer; // Quay lại trạng thái FollowPlayer nếu không còn target
        }
    }

    void AttackTarget()
    {
        if (target != null)
        {
			// Nếu kẻ thù đã chết
			if (target.GetComponent<Enemy>().Die())
			{
				Transform oldTarget = target;
                target = null;
				FindTarget(); // Kiểm tra xem có kẻ thù khác trong phạm vi không
				if (target == oldTarget)
				{
					currentState = State.FollowPlayer; // Quay lại trạng thái FollowPlayer nếu không còn target
				}
			}

			attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                PerformComboAttack();
                attackTimer = 0;

                // Thực hiện hành vi tấn công (ví dụ: giảm máu kẻ thù)***
                Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
                
                if (hitEnemy != null)
                {
					target.GetComponent<Enemy>().TakeDamage(attackDamage);
				}
            }
        }
        else
        {
            currentState = State.FollowPlayer; // Quay lại trạng thái FollowPlayer nếu không còn target
        }
    }

    void PerformComboAttack()
    {
        int comboIndex = Random.Range(0, attackCombos.Length);
        animator.SetTrigger(attackCombos[comboIndex]);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(WaitBeforeDisappear());
        Destroy(gameObject);
    }

    IEnumerator WaitBeforeDisappear()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("isDead", true);
    }
    //***
    private void FlipCharacter(float moveDirection)
    {
        if (moveDirection > 0 && !facingRight || moveDirection < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);//***
    }
}


