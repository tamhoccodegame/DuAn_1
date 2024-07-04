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

    private int comboStep = 1;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;
    public float attackCooldown = 0.5f;
    private bool inputReceived = false;

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

        Debug.Log(isAttacking);

		Combo();
		
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

    public void StartCombo()
    {
        isAttacking = false;
        if (comboStep < 3)
            comboStep++;
        if (inputReceived)
        {
			isAttacking = true;
			animator.SetTrigger("isAttack" + comboStep);
			inputReceived = false;
        }
    }

    public void EndCombo()
    {
        //animator.ResetTrigger("isAttack" + comboStep);
        comboStep = 1;
        //StartCoroutine(AttackCooldown());
        isAttacking = false;
    }

    void Combo()
    {
		if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
		{
            isAttacking = true;
            animator.SetTrigger("isAttack" + comboStep);
		}
        else if(Input.GetKeyDown(KeyCode.J) && isAttacking)
        {
            inputReceived = true;
        }
        
	}

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
