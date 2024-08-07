using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_Health : MonoBehaviour
{
    public bool isVulnerable = true;
    public int maxHealth = 200;
    public int currentHealth;
    public Player_HealthBar player_HealthBar;

    Animator animator;
    Rigidbody2D rig;
    CapsuleCollider2D col;

    public ParticleSystem player_Hurt_Effect;
    public ParticleSystem player_Blood_Effect;
    public ParticleSystem player_Death_Effect;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player_HealthBar = GameSession.instance.GetPlayer_HealthBar();
        player_HealthBar.SetMaxHealth(maxHealth);
        player_HealthBar.SetHealth(maxHealth);
        col = GetComponent<CapsuleCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestoreHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
		player_HealthBar.SetHealth(currentHealth);
	}

    public IEnumerator TakeDamage(int damage)
    {
        currentHealth -= damage;

		FindObjectOfType<SoundManager>().PlayAudio("Player_Hurt");
		player_Blood_Effect.Play();
		animator.ResetTrigger("isAttack1");
		animator.ResetTrigger("isAttack2");
		animator.ResetTrigger("isAttack3");
		GetComponent<PlayerController>().EndCombo();

		animator.SetTrigger("isHurt");
		Instantiate(player_Hurt_Effect, transform.position, Quaternion.identity, transform);
		player_HealthBar.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			StartCoroutine(Die());
            yield break;
		}


        //FindObjectOfType<SoundManager>().PlayAudio("Player_Hurt");

        yield return null;

        float stopTime = .3f;

        yield return StartCoroutine(StopTime(stopTime));

    }

    private IEnumerator StopTime(float duration)
	{
		// Dừng thời gian
		Time.timeScale = 0f;

		// Lưu thời gian bắt đầu
		float startTime = Time.realtimeSinceStartup;

		// Đợi cho đến khi đủ thời gian trôi qua
		while (Time.realtimeSinceStartup - startTime < duration)
		{
			// Đợi một chút để không làm treo game
			yield return null;
		}

		// Khôi phục thời gian
		Time.timeScale = 1f;
	}


    IEnumerator Die()
    {
        FindObjectOfType<SoundManager>().PlayAudio("Player_Death");
        animator.SetBool("isDead", true);
        //player_Death_Effect.Play();
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach(Collider2D col in colliders)
        {
            col.enabled = false;
        }

        //GetComponent<Collider2D>().enabled = false; //Disable the collider 2D
        this.enabled = false;
        //transform.position = Checkpoint_System.Instance.GetLastCheckpointPosition();
        //deathEffect.Play();
        //FindObjectOfType<GameSession>().PlayerDeath();
        yield return new WaitForSeconds(4f);
        GameSession.instance.LoadCheckpoint();
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(2f);
        Checkpoint_System.Instance.Respawn(transform);
        //Checkpoint_System checkpoint = GetComponent<Checkpoint_System>();
        //checkpoint.Respawn();
        currentHealth = maxHealth;
        player_HealthBar.SetHealth(currentHealth);
        animator.SetBool("isDead", false);

    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Enemy"))
        {          
            GetComponent<Player_Health>().TakeDamage(25);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }
}
