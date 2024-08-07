using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriestBoss : Enemy
{
	private int currentComboStrikes;

	[SerializeField] private int speed;
	[SerializeField] private float _attackRange;
	[SerializeField] private bool isRage = false;
	[SerializeField] private float _maxHealth;

	[SerializeField] private GameObject fireBallPrefab;
	[SerializeField] private GameObject dustPrefabs;
	[SerializeField] private GameObject rockPrefabs;
	[SerializeField] private GameObject effectPoint;
	[SerializeField] private GameObject dustPoint;

	[SerializeField] private Slider healthBar_slider;


	// Start is called before the first frame update
	//Combo 1: Dam 1 + (true, false) Dam 2
	//Combo 2: Chem 1 + (true, false) Chem 2
	//Combo 3: Dam 1 + Chem 1
	//Combo 4: Dam dat ban song 2 lan

	public override void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//currentComboStrikes = Random.Range(1, 4);
		currentComboStrikes = 1;
		direction = Vector2.right;
		attackRange = _attackRange;
		maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		healthBar_slider = GameSession.instance.GetBossHealthBar();
	}

	private void Start()
	{
		healthBar_slider.maxValue = maxHealth;
		healthBar_slider.value = maxHealth;
	}

	private void RandomComboStrike()
	{
		currentComboStrikes = Random.Range(1, 5);
	}

    // Update is called once per frame
    public override void Update()
    {
        if (isCoroutineRunning) return;

		direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();
		LookAtDirection(direction);

		isCoroutineRunning = true;	
        StartCoroutine("Combo" + currentComboStrikes);


		if (currentHealth <= 0.5f * maxHealth && !isRage)
		{
			isRage = true;
			Instantiate(vialityEffect, transform.position, Quaternion.identity);
		}
	}

	public override void Chase()
	{
		animator.Play("Walk");
		

		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

	}

	//Punch 1 punch 2
	private IEnumerator Combo1()
	{
		if(Mathf.Abs(player.transform.position.x - transform.position.x) <= attackRange)
		{
			rb.velocity = Vector2.zero;
			animator.Play("Punch_1");
			int shouldPunch = Random.Range(0, 2);
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .5f);
			if (shouldPunch == 1)
			{
				animator.Play("Punch_2");
				yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
			}
			animator.Play("Idle");
			yield return new WaitForSeconds(.8f);
			RandomComboStrike();
			isCoroutineRunning = false;
		}
		else
		{
			Chase();
			isCoroutineRunning = false;
		}
		
	}

	public void FireBall()
	{
		Rigidbody2D fireBall = Instantiate(fireBallPrefab, effectPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
		fireBall.transform.localScale = new Vector3(direction.x * fireBall.transform.localScale.x,
															fireBall.transform.localScale.y,
															 fireBall.transform.localScale.z);
		StartCoroutine(PlayBallSound());
		fireBall.AddForce(new Vector2(direction.x * 10f, 0), ForceMode2D.Impulse);
		Destroy(fireBall.gameObject, 2f);
	}

	IEnumerator PlayBallSound()
	{
		PlaySound("PriestBall");
		yield return new WaitForSeconds(1f);
		StopSound("PriestBall");
	}

	
	public void DustWave()
	{
		Camera cam = Camera.main;

		Vector3 viewportCheck = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, cam.nearClipPlane));
		Vector3 spawnLocation = new Vector3(Random.Range(0, viewportCheck.x), viewportCheck.y, 0);

		for (int i = 0; i < 3; i++)
		{
			var go = Instantiate(rockPrefabs, spawnLocation, Quaternion.identity);
			spawnLocation = new Vector3(Random.Range(0, viewportCheck.x), viewportCheck.y, 0);
			Destroy(go, 1f);
		}
		Rigidbody2D dustOne = Instantiate(dustPrefabs, dustPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
		Rigidbody2D dustTwo = Instantiate(dustPrefabs, dustPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

		dustOne.transform.localScale = new Vector3(direction.x * dustOne.transform.localScale.x,
													dustOne.transform.localScale.y,
													dustOne.transform.localScale.z);

		dustTwo.transform.localScale = new Vector3(-direction.x * dustTwo.transform.localScale.x,
													dustTwo.transform.localScale.y,
													dustTwo.transform.localScale.z);

		dustOne.AddForce(new Vector2(direction.x * 10f, 0), ForceMode2D.Impulse);
		dustTwo.AddForce(new Vector2(-direction.x * 10f, 0), ForceMode2D.Impulse);

		Destroy(dustOne.gameObject, 1f);
		Destroy(dustTwo.gameObject, 1f);
	}


	//Don quay riu bo sung hieu ung 
	private IEnumerator Combo2()
	{
		if (Mathf.Abs(player.transform.position.x - transform.position.x) <= attackRange)
		{
			rb.velocity = Vector2.zero;
			animator.Play("Slash_1");
			int shouldPunch = Random.Range(0, 2);
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .5f);
			if (shouldPunch == 1)
			{
				animator.Play("Slash_2");
				yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
			}
			animator.Play("Idle");
			yield return new WaitForSeconds(.8f);
			RandomComboStrike();
			isCoroutineRunning = false;
		}
		else
		{
			Chase();
			isCoroutineRunning = false;
		}

	}

	private IEnumerator Combo3()
	{
		if (Mathf.Abs(player.transform.position.x - transform.position.x) <= attackRange)
		{
			rb.velocity = Vector2.zero;
			animator.Play("Punch_1");
			int shouldPunch = Random.Range(0, 2);
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + 1f);
			if (shouldPunch == 1)
			{
				animator.Play("Slash_1");
				yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .5f);
			}
			animator.Play("Idle");
			yield return new WaitForSeconds(.8f);
			RandomComboStrike();
			isCoroutineRunning = false;
		}
		else
		{
			Chase();
			isCoroutineRunning = false;
		}
	}

	//Dam dat bo sung dot song va da roi
	private IEnumerator Combo4()
	{
		for (int i = 0; i < Random.Range(2,5); i++)
		{
			animator.Play("CastSkill");
			PlaySound("Rock_Falling");
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .5f);


			animator.Play("Idle");
			yield return new WaitForSeconds(1f);
		}

		RandomComboStrike();
		isCoroutineRunning = false;
		
	}

	public override void TakeDamage(float damage)
	{
		if (!isAlive) return;
		base.TakeDamage(damage);
		healthBar_slider.value = currentHealth;
	}

	public override void DropCoin()
	{
		for (int i = 0; i < 1000; i++)
		{
			Instantiate(coinPrefab, transform.position + new Vector3(Random.Range(1, 6), 0, 0), transform.rotation);
		}
	}

	public override bool Die()
	{
		Equipment equipment = GameSession.instance.GetEquipment();
		equipment.IncreaseSlot();
		isAlive = false;
		this.enabled = false;
		StartCoroutine(DieDelay());
		GameObject.Find("BlockDoorEffect").SetActive(false);
		healthBar_slider.transform.parent.gameObject.SetActive(false);
		return true;
	}
}
