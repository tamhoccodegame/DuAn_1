using UnityEngine;
using UnityEngine.AI;
using System.Collections;



public class Teammate_Behavior : MonoBehaviour
{
    public float followDistance = 2f;
    public float moveSpeed = 5f;
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

    private enum State
    {
        Idle,
        FollowPlayer,
        AttackEnemy
    }

    private State currentState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            case State.AttackEnemy:
                AttackTarget();
                break;
        }

        if (currentState != State.AttackEnemy)
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
            currentState = State.AttackEnemy;
        }
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > followDistance)
        {
            animator.SetBool("isRunning", true);
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * followDistance * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero; // Ngừng di chuyển khi đến gần player
        }
    }

    void AttackTarget()
    {
        if (target != null)//Da tim thay enemy trong pham vi tan cong
        {
            //if (Vector2.Distance(transform.position, target.transform.position) > attackRange)
            //{
            //    animator.SetBool("isRunning", true);
            //    transform.position = Vector2.MoveTowards(transform.position, target.position, followDistance * Time.deltaTime * moveSpeed);
            //    //Vector2 direction = (target.position - transform.position).normalized;
            //    //rb.MovePosition(rb.position + direction * moveSpeed * followDistance * Time.deltaTime);
            //}


            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                PerformComboAttack();
                attackTimer = 0;

                // Thực hiện hành vi tấn công (ví dụ: giảm máu kẻ thù)
                target.GetComponent<Enemy>().TakeDamage(attackDamage);

                // Nếu kẻ thù đã chết
                if (target.GetComponent<Enemy>().Die())
                {
                    target.GetComponent<Enemy>().Die();
                    target = null;
                    currentState = State.FollowPlayer; // Quay lại trạng thái FollowPlayer
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

