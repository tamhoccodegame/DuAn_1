using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
	private int currentComboStrikes;

	private int speed = 15;
	private int countTouchWall = 0;
	
	public override void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//currentComboStrikes = Random.Range(1, 4);
		currentComboStrikes = 1;
		direction = Vector2.right;
	}

	public override void Update()
	{
		if (isCoroutineRunning) return;
		switch (currentComboStrikes)
		{
			case 1:
				isCoroutineRunning = true;
				StartCoroutine(Combo1());
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			direction.x *= -1;
			Debug.Log(direction);
			countTouchWall++;
		}
	}

	private void RandomComboStrike()
	{
		//currentComboStrikes = Random.Range(1, 4);
	}

	//Running left and right agressively
	private IEnumerator Combo1()
	{
		if(countTouchWall < 4)
		{
			rb.velocity = new Vector3(speed * direction.x, 0, 0);
			yield return new WaitForSeconds(.1f);
			isCoroutineRunning = false;
		}
		else
		{
			Debug.Log("Qua bien gioi roi");
			animator.Play("Idle");
			rb.velocity = Vector2.zero;
			yield return new WaitForSeconds(2f);
			RandomComboStrike();
			Debug.Log("Combo1 done, wait for random currentComboStrike");
			isCoroutineRunning = false;
		}

	}

	//Chase Player and Attack
	private IEnumerator Combo2()
	{
		yield return null;
	}

	//CastSkill Thunder
	private IEnumerator Combo3()
	{
		yield return null;
	}

	//Random the others Combo with less duration but more speed.
	private IEnumerator Combo4()
	{
		yield return null;
	}
}
