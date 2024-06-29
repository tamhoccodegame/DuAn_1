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



}
