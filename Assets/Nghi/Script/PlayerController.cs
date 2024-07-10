using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 currentInput;
    private Vector2 pendingInput;
    public bool isAlive = true;
    public bool facingRight = true;
    private bool isJumping = false; 

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
    public float attackCooldown = 0.1f;
    private bool inputReceived = false;

    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCooldown;
    private float dashTimeLeft;
    private float lastImageXposition;
    private float lastDash = -100f;

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

        //if(isAttacking) return;

        pendingInput = value.Get<Vector2>();

        if (!isAttacking) currentInput = pendingInput;
    }

    void OnJump(InputValue value)
    {
        if (isAlive == false) return;

        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }
        if (value.isPressed && !isAttacking) 
        {
            rig.velocity += new Vector2(0f, jump);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isJumping);
        if (isAlive == false) return;

        Combo();

        Run();
        

        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        animator.SetBool("isRunning", havemove);
        animator.SetBool("isAttacking", isAttacking);

        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }

        //Debug.Log(isAttacking);

		
                		
    }

    void Run()
    {
        moveInput = currentInput;
		rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJump", false);
            isJumping = false;
        }
        else
        {
            animator.SetBool("isJump", true);
            isJumping = true;
        }

    }

    void Flip()
    {
        if (isAlive == false) return;
        
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
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
        animator.ResetTrigger("isAttack1");
		animator.ResetTrigger("isAttack2");
		animator.ResetTrigger("isAttack3");
        animator.ResetTrigger("isJumpAttack");
        comboStep = 1;
        isAttacking = false;
        currentInput = pendingInput;
    }

    void Combo()
    {
		if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
		{
			isAttacking = true;
            if (isJumping) StartCoroutine(SpecialAttack("isJumpAttack"));
            else if (Mathf.Abs(rig.velocity.x) >= 1.0f) StartCoroutine(SpecialAttack("isRunAttack"));

            else if (moveInput.y >= 1) StartCoroutine(SpecialAttack("isAirAttack"));
            else animator.SetTrigger("isAttack" + comboStep);
		}
        else if(Input.GetKeyDown(KeyCode.J) && isAttacking)
        {
            inputReceived = true;
        }
	}

    IEnumerator StopMotion(float time)
    {
        currentInput = Vector2.zero;
        yield return new WaitForSeconds(time);
        currentInput = pendingInput;
    }

    IEnumerator SpecialAttack(string name)
    {
        animator.SetTrigger(name);
        if (!isJumping)
        {
            EndCombo();
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        EndCombo();
	}

	IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
