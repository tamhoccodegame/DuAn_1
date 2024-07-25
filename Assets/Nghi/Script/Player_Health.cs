using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public Player_HealthBar player_HealthBar;

    Animator animator;
    Rigidbody2D rig;
    CapsuleCollider2D col;

    public ParticleSystem player_Blood_Effect;
    public ParticleSystem player_Death_Effect;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player_HealthBar.SetMaxHealth(maxHealth);
        col = GetComponent<CapsuleCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        player_HealthBar.SetHealth(currentHealth);

        animator.SetTrigger("isHurt");
        player_Blood_Effect.Play();
        animator.ResetTrigger("isAttack1");
		animator.ResetTrigger("isAttack2");
		animator.ResetTrigger("isAttack3");
        GetComponent<PlayerController>().EndCombo();
		//FindObjectOfType<SoundManager>().PlayAudio("Player_Hurt");
		//blood.Play();

		if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
     
        animator.SetBool("isDead", true);
        player_Death_Effect.Play();
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach(Collider2D col in colliders)
        {
            col.enabled = false;
        }

        //GetComponent<Collider2D>().enabled = false; //Disable the collider 2D
        this.enabled = false;

        //deathEffect.Play();
        //FindObjectOfType<GameSession>().PlayerDeath();
    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Spike"))
        {          
            GetComponent<Player_Health>().TakeDamage(25);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }
}
