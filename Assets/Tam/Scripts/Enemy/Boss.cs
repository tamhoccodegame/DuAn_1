using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
	[SerializeField] private GameObject effectPrefabs;
	[SerializeField] private GameObject rockPrefabs;
	public Transform effectPoint;

	private int currentComboStrikes;

	private int speed = 25;
	private int countTouchWall = 0;
	private int countChasePlayer = 0;
	private float _attackRange = 5f;
	private bool isRage = false;

	[SerializeField] private float _maxHealth;
	public Slider healthBar_slider;
	
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
	}

	private void Start()
	{
		healthBar_slider.maxValue = maxHealth;
		healthBar_slider.value = maxHealth;
		direction = direction = new Vector3(player.position.x - transform.position.x, 0, 0);
	}
	public override void Update()
	{

		if (isCoroutineRunning) return;

		if ((currentHealth <= 0.5 * maxHealth) && !isRage)
		{
			isRage = true;	
			speed += 5;
			var go = Instantiate(vialityEffect, transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
			go.transform.rotation = Quaternion.Euler(-90f, 0, 0);
		}

		isCoroutineRunning = true;
		StartCoroutine("Combo" + currentComboStrikes);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			direction.Normalize();
			direction.x *= -1;
			transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

			Debug.Log(direction);
			countTouchWall++;

			Camera cam = Camera.main;

			Vector3 viewportTopLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
			Vector3 viewportTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

			PlaySound("Rock_Falling");
			for (int i = 0; i < 3; i++)
			{
				float randomX = Random.Range(viewportTopLeft.x, viewportTopRight.x);
				Vector3 spawnLocation = new Vector3(randomX, viewportTopLeft.y, 0);
				var go = Instantiate(rockPrefabs, spawnLocation, Quaternion.identity);
				Destroy(go, 1f);
			}


			isCoroutineRunning = false;
		}
	}

	private void RandomComboStrike()
	{
		currentComboStrikes = Random.Range(1, 4);
	}

	//Running left and right agressively
	private IEnumerator Combo1()
	{
		if(countTouchWall < 4)
		{
			direction.Normalize();
			animator.Play("Move");
			rb.velocity = new Vector3(speed * direction.x, 0, 0);
		}
		else
		{
			animator.Play("Idle");
			rb.velocity = Vector2.zero;
			yield return new WaitForSeconds(2f);
			RandomComboStrike();
			yield return new WaitForSeconds(.5f);
			Debug.Log("Combo1 done, wait for random currentComboStrike");
			countTouchWall = 0;
			isCoroutineRunning = false;
		}

	}

	
	public override void Chase()
	{
		animator.Play("Move");
		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

	}


	//Chase Player and Attack
	private IEnumerator Combo2()
	{
		if(countChasePlayer < 4)
		{
			if (Mathf.Abs(player.position.x - transform.position.x) <= attackRange)
			{
				countChasePlayer++;
				rb.velocity = Vector2.zero;
				animator.Play("Attack");
				yield return new WaitForSeconds(.5f);
				animator.Play("Idle");
				yield return new WaitForSeconds(1f);
				isCoroutineRunning = false;
			}
			else
			{
				Chase();
				isCoroutineRunning = false;
			}
			
		}
		else
		{
			animator.Play("Idle");
			rb.velocity = Vector2.zero;
			yield return new WaitForSeconds(1f);
			RandomComboStrike();
			yield return new WaitForSeconds(.5f);
			countChasePlayer = 0;
			isCoroutineRunning = false;
		}
		
	}

	//CastSkill Thunder
	private IEnumerator Combo3()
	{
		animator.Play("CastSkill");
		yield return new WaitForSeconds(.5f);
		Camera cam = Camera.main;
		Vector3 topBorder = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, cam.nearClipPlane));
		Vector3 startPos = new Vector3(effectPoint.position.x, topBorder.y, 0);

		var go = Instantiate(effectPrefabs, startPos, Quaternion.identity);
		yield return new WaitForSeconds(.3f);
		Destroy(go);

		Vector3 offset = new Vector3(2 * direction.x, 0, 0);
		Vector3 viewportCheck = cam.WorldToScreenPoint(go.transform.position);

		while(viewportCheck.x >= 0 && viewportCheck.x <= Screen.width)
		{
			var go1 = Instantiate(effectPrefabs, (startPos + offset), Quaternion.identity);
			startPos = startPos + offset;
			viewportCheck = cam.WorldToScreenPoint(go1.transform.position);
			PlaySound("HeadlessSkill");
			yield return new WaitForSeconds(.3f);
			StopSound("HeadlessSkill");
			Destroy (go1);
		}

		animator.Play("Idle");
		yield return new WaitForSeconds(1.5f);
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
		healthBar_slider.transform.parent.gameObject.SetActive(false);
		GameObject.Find("BlockDoorEffect").SetActive(false);
		StartCoroutine(DieDelay());
		
		return true;
	}

}
