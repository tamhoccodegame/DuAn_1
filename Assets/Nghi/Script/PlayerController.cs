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
    [SerializeField] private int damage;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 1f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 1f;
    private bool isAttacking = false;
    public float attackCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    public int GetDamage()
    {
        return damage;
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
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Attack3();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //COMBO
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }


        if (Input.GetKeyDown(KeyCode.U) && !isAttacking)
        {
            lastAttackTime = Time.time;
            Combo();
        }

        // Reset isAttacking flag when the current animation is over
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag("Attack") && stateInfo.normalizedTime >= 1.0f)
        {
            isAttacking = false;
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
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        ////Damage Enemies
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    Debug.Log("Hit " + enemy.name);
        //    enemy.GetComponent<Enemy>().TakeDamage(10); //attackDamage
        //}
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
        animator.SetTrigger("isAttack3");
        //FindObjectOfType<SoundManager>().PlayAudio("Player_Attack");

        //Detect enemies in range of attack
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

    void Combo()
    {
        comboStep++;
        if (comboStep == 1)
        {
            animator.SetTrigger("isAttack4");
            //Detect enemies in range of attack
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            ////Damage Enemies
            //foreach (Collider2D enemy in hitEnemies)
            //{
            //    Debug.Log("Hit " + enemy.name);
            //    enemy.GetComponent<Enemy>().TakeDamage(15); //attackDamage
            //}
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("isAttack5");
            //Detect enemies in range of attack
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            ////Damage Enemies
            //foreach (Collider2D enemy in hitEnemies)
            //{
            //    Debug.Log("Hit " + enemy.name);
            //    enemy.GetComponent<Enemy>().TakeDamage(25); //attackDamage
            //}
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("isAttack6");
            //Detect enemies in range of attack
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            ////Damage Enemies
            //foreach (Collider2D enemy in hitEnemies)
            //{
            //    Debug.Log("Hit " + enemy.name);
            //    enemy.GetComponent<Enemy>().TakeDamage(35); //attackDamage
            //}

            comboStep = 0; // Reset combo after the final attack

            isAttacking = true;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
