using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	protected Transform player;

    protected float maxHealth;
    protected float currentHealth;
	protected float speed;

	protected Rigidbody2D rb;
    protected Animator animator;
     
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
 
    }

    protected void Attack()
    {
        animator.SetTrigger("isAttack"); 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.enabled = false;
    }


}
