using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	private void RandomComboStrike()
	{
		currentComboStrikes = Random.Range(1, 5);
	}

    // Update is called once per frame
    public override void Update()
    {
        if (isCoroutineRunning) return;

		isCoroutineRunning = true;	
        StartCoroutine("Combo" + currentComboStrikes);
    }

	public override void Chase()
	{
		animator.Play("Walk");
		direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();

		LookAtDirection(direction);

		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

	}

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
		fireBall.AddForce(new Vector2(direction.x * 10f, 0), ForceMode2D.Impulse);
		Destroy(fireBall.gameObject, 2f);
	}

	
	public void DustWave()
	{
		Camera cam = Camera.main;

		Vector3 viewportCheck = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, cam.nearClipPlane));
		Vector3 spawnLocation = new Vector3(Random.Range(0, viewportCheck.x), viewportCheck.y, 0);

		Instantiate(rockPrefabs, spawnLocation, Quaternion.identity);
		spawnLocation = new Vector3(Random.Range(0, viewportCheck.x), viewportCheck.y, 0);
		Instantiate(rockPrefabs, spawnLocation, Quaternion.identity);
		spawnLocation = new Vector3(Random.Range(0, viewportCheck.x), viewportCheck.y, 0);
		Instantiate(rockPrefabs, spawnLocation, Quaternion.identity);

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
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .5f);


			animator.Play("Idle");
			yield return new WaitForSeconds(1f);
		}

		RandomComboStrike();
		isCoroutineRunning = false;
		
	}

}
