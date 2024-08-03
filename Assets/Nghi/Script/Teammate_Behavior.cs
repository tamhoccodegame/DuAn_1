using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Teammate_Behavior : MonoBehaviour
{
    public float followDistance = 2f;
    public float attackRange = 10f;
    public float attackInterval = 1f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;
    public int maxHealth = 100;
    private int currentHeath;

    private Transform playerTranform;
    private Transform target;
    private float attackTimer;

    Animator animator;
    public string[] attackCombos;
    // Start is called before the first frame update
    void Start()
    {
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHeath = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if (target != null)
        {
            AttackTarget();
        }
        else
        {
            FollowPlayer();
        }
    }

    public void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        if (enemies.Length > 0)
        {
            target = enemies[0].transform;
        }
        else
        {
            target = null;
        }
    }

    public void AttackTarget()
    {
        if (target != null)
        {
            attackTimer = Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                PerformComboAttack();
                attackTimer = 0;

                if (target.GetComponent<Enemy>().Die())
                {
                    target = null;
                }
            }
        }
    }

    public void PerformComboAttack()
    {
        int comboIndex = Random.Range(0, attackCombos.Length);
        animator.SetTrigger(attackCombos[comboIndex]);
        target.GetComponent<Enemy>().TakeDamage(attackDamage);
    }

    public void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTranform.position);
        if (distanceToPlayer > followDistance)
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, playerTranform.position, attackRange * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHeath -= maxHealth;
        if (currentHeath <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
    }

    IEnumerator WaitBeforeDisappear()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
