using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public bool isAlive = true;
    public bool facingRight = true;

    Rigidbody2D rig;
    Animator animator;
    CapsuleCollider2D col;
    public BoxCollider2D feet;
    [SerializeField] float speed = 10f;
    [SerializeField] float jump = 20f;

    //public Transform attackPoint;/
    public LayerMask enemyLayers;
    public float attackRange = 1f;
    private int damage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;


    public int GetDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        if (isAlive == false) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isAlive == false) return;

        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return; 
        }
        if (value.isPressed) 
        {
            rig.velocity += new Vector2(0f, jump);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false) return;

        Run();

        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", havemove);

        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }


        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack1();
                nextAttackTime = Time.time + 1.5f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void Run()
    {
        if (isAlive == false) return;

        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJump", false);
        }
        else
        {
            animator.SetBool("isJump", true);
        }

    }

    void Flip()
    {
        if (isAlive == false) return;
        
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Attack1()
    {
        //PLAY ATTACK ANIMATION
        animator.SetTrigger("isAttack1");
        //FindObjectOfType<SoundManager>().PlayAudio("Player_Attack");
        //Detect enemies in range of attack

    }

    void Attack2()
    {
        //PLAY ATTACK ANIMATION
        animator.SetTrigger("isAttack2");
        //FindObjectOfType<SoundManager>().PlayAudio("Player_Attack");
        //Detect enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        ////Damage Enemies
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    Debug.Log("Hit " + enemy.name);
        //    enemy.GetComponent<Enemy>().TakeDamage(20); //attackDamage
        //}
    }

    void Attack3()
    {
        //PLAY ATTACK ANIMATION
        //animator.SetTrigger("isAttack3");
        ////FindObjectOfType<SoundManager>().PlayAudio("Player_Attack");

        ////Detect enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        ////Damage Enemies
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    Debug.Log("Hit " + enemy.name);
        //    enemy.GetComponent<Enemy>().TakeDamage(30); //attackDamage
        //}
    }

    private void OnDrawGizmosSelected()//Ve Gizmos de xac dinh AttackPoint
    {
        //if (attackPoint == null) return;
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
