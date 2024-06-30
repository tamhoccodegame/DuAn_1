using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public Player_HealthBar player_HealthBar;

    public Animator animator;
    Rigidbody2D rig;
    CapsuleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player_HealthBar.SetMaxHealth(maxHealth);
        rig = GetComponent<Rigidbody2D>();
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
        GetComponent<Collider2D>().enabled = false; //Disable the collider 2D
        this.enabled = false;
        
        //deathEffect.Play();
        FindObjectOfType<GameSession>().PlayerDeath();
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
