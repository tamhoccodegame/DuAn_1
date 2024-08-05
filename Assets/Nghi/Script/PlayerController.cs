using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
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
    public bool canDoubleJump = false;
    public float jumpCount;

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

    private DashAfterImage dashAfterImage;
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;

    public ParticleSystem coinEffect;

    public GameObject snakePrefab;
    private float snakeLifetime = 1f;
    private float summonRange = 10f;

    public GameObject teammatePrefab;
    public Transform summonTeammatePosition;//Vi tri trieu hoi dong doi
    public float summonTime = 30f;//Thoi gian ton tai cua dong doi
    private GameObject currentTeammate;

    public Skill_Mana skillManager;

    //private bool isDashing;
    //public float dashTime;
    //public float dashSpeed;
    //public float distanceBetweenImages;
    //public float dashCooldown;
    //private float dashTimeLeft;
    //private float lastImageXposition;
    //private float lastDash = -100f;
    //private bool canMove = true;
    //private bool canFlip = true;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        dashAfterImage = GetComponent<DashAfterImage>();

        if (skillManager == null)
        {
            skillManager = FindObjectOfType<Skill_Mana>();
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
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

        //if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        //{
        //    return;
        //}

        //if (value.isPressed && !isAttacking)
        //{
        //    rig.velocity += new Vector2(0f, jump);
        //}
        
        if (canDoubleJump)
        {
			if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")) || jumpCount < 1)
			{
				if (value.isPressed)
				{
					rig.velocity += new Vector2(0f, jump);
                    FindObjectOfType<SoundManager>().PlayAudio("Player_Jump");
                    jumpCount++;
				}
			}
		}
        else
        {
			if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
				if (value.isPressed)
				{
					rig.velocity += new Vector2(0f, jump);
                    FindObjectOfType<SoundManager>().PlayAudio("Player_Jump");
                }
			}
			
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isJumping);***
        if (isAlive == false) return;

        Combo();

        Run();
        

        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        animator.SetBool("isRunning", havemove);



        if (havemove && feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            ////huong cua player
            //int huong = (int)transform.localScale.x;
            ////lay rotate cua dust
            //Quaternion rotatedust = smokeEffect.transform.localRotation;
            //if (huong == 1)
            //    rotatedust.y = 180;
            //else if (huong == -1)
            //    rotatedust.y = 0;
            //smokeEffect.transform.localRotation = rotatedust;//cap nhat
            //smokeEffect.Play();
        }
        animator.SetBool("isAttacking", isAttacking);

        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }

        //Debug.Log(isAttacking);
        //Dash();
        //CheckDash();
        SnakeAttack();
        SummonTeammateWhenPressButton();
    }

    private IEnumerator Dash()
    {
        dashAfterImage.StartDashing();
        FindObjectOfType<SoundManager>().PlayAudio("Player_Dash");
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            transform.Translate(Vector3.right * dashSpeed * Time.deltaTime);
            yield return null;
        }
        GetComponent<Player_StaminaSystem>().ReduceStamina(10);
        dashAfterImage.StopDashing();
        
    }

    void Run()
    {
        moveInput = currentInput;
		rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJump", false);
            isJumping = false;
            jumpCount = 0; //Reset jumpCount
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

    //void Dash()
    //{
    //    if (Input.GetButtonDown("Dash"))
    //    {
    //        if (Time.time > (lastDash + dashCooldown)) 
    //        AttempToDash();
    //    }
    //}

    //private void AttempToDash()
    //{
    //    isDashing = true;
    //    dashTimeLeft = dashTime;
    //    lastDash = Time.time;

    //    PlayerAfterImagePool.Instance.GetFromPool();
    //    lastImageXposition = transform.position.x;
    //}

    //private void CheckDash()
    //{
    //    if (isDashing)
    //    {
    //        if (dashTimeLeft > 0)
    //        {
    //            canMove = false;
    //            canFlip = false;
    //            rig.velocity = new Vector2(dashSpeed, rig.velocity.y);
    //            dashTimeLeft -= Time.deltaTime;

    //            if (Mathf.Abs(transform.position.x - lastImageXposition) > distanceBetweenImages)
    //            {
    //                PlayerAfterImagePool.Instance.GetFromPool();
    //                lastImageXposition = transform.position.x;
    //            }
    //        }

    //        if(dashTimeLeft <= 0)
    //        {
    //            isDashing = false;
    //            canFlip = true;
    //            canMove = true;
    //        }
    //    }
    //}

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

    public void PlayAttackSound(string soundName)
    {
        FindObjectOfType<SoundManager>().PlayAudio(soundName);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            //var go = Instantiate(coinEffect, transform.position, transform.rotation);
            Debug.Log("Coin collected!");
            FindObjectOfType<SoundManager>().PlayAudio("Coin_Collect");
            Destroy(collision.gameObject);
            FindObjectOfType<GameSession>().AddCoin(1);
        }
        if (collision.CompareTag("Next_Level"))
        {
            Animator doorAnimation = collision.GetComponent<Animator>();

            if (doorAnimation != null)
            {
                FindObjectOfType<SoundManager>().PlayAudio("Next_Level");
                doorAnimation.SetTrigger("Open");
            }

        }
    }

    public GameObject FindNearestEnemy(Vector3 playerPosition)
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = summonRange;
        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);
            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public void SummonSnakeAtEnemy(Vector3 playerPosition)
    {
        GameObject nearestEnemy = FindNearestEnemy(playerPosition);
        if (nearestEnemy != null)
        {
            Vector3 nearestEnemyPosition = nearestEnemy.transform.position;
            SpawnSnakeAtEnemy(nearestEnemyPosition);
        }
        else
        {
            Debug.Log("Not found any enemy in range");
        }
    }

    public void SpawnSnakeAtEnemy(Vector3 enemyPosition)
    {
        GameObject snake = Instantiate(snakePrefab, enemyPosition, Quaternion.identity);
        Destroy(snake, snakeLifetime);
    }

    public void SnakeAttack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SummonSnakeAtEnemy(transform.position);
            skillManager.UseSkill(25f);
            FindObjectOfType<SoundManager>().PlayAudio("Snake_Attack");
        }
    }

    public void SummonTeammate()
    {
        if(currentTeammate == null)
        {
            currentTeammate = Instantiate(teammatePrefab, summonTeammatePosition.position, summonTeammatePosition.rotation);
            skillManager.UseSkill(50f);
            StartCoroutine(DismissTeammateAfterTime(summonTime)); 
        }
        else
        {
            Debug.Log("Teammate is already summoned!");
        }
    }

    private IEnumerator DismissTeammateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (currentTeammate != null)
        {
            Destroy(currentTeammate);
        }
    }

    public void SummonTeammateWhenPressButton()
    {
        if (Input.GetKey(KeyCode.O))
        {
            SummonTeammate();
        }
    }

    public void SkillManaUpdate(int damage)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            skillManager.AddSkillMana(damage);
        }
        
    }
}
