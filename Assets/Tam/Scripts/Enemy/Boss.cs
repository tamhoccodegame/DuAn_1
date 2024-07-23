using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
	[SerializeField] private GameObject effectPrefabs;
	public Transform effectPoint;

	private int currentComboStrikes;

	private int speed = 15;
	private int countTouchWall = 0;
	private int countChasePlayer = 0;
	private float _attackRange = 5f;
	
	public override void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//currentComboStrikes = Random.Range(1, 4);
		currentComboStrikes = 1;
		direction = Vector2.right;
		attackRange = _attackRange;
	}

	public override void Update()
	{
		Debug.Log(countTouchWall);

		if (isCoroutineRunning) return;

		switch (currentComboStrikes)
		{
			case 1:
				isCoroutineRunning = true;
				StartCoroutine(Combo1());
				break;
			case 2:
				isCoroutineRunning = true;
				StartCoroutine(Combo2());
				break;
			case 3:
				isCoroutineRunning = true;
				StartCoroutine(Combo3());
				break;
			case 4:
				break;
		}
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
			animator.Play("Move");
			rb.velocity = new Vector3(speed * direction.x, 0, 0);
			isCoroutineRunning = false;
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
		direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();

		LookAtDirection(direction);

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
		direction = new Vector3(player.position.x - transform.position.x, 0, 0);
		direction.Normalize();

		LookAtDirection(direction);

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
			yield return new WaitForSeconds(.3f);
			Destroy (go1);
		}

		animator.Play("Idle");
		yield return new WaitForSeconds(1.5f);
		RandomComboStrike();
		isCoroutineRunning = false;	
	}

	//Random the others Combo with less duration but more speed.
	private IEnumerator Combo4()
	{
		yield return null;
	}
}
