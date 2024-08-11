using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GiantGirlBoss : Enemy
{
	#region DataMembers
	[SerializeField] private float _maxHealth;
	private int currentComboStrikes;
	public Slider healthBar_slider;
	private bool isRage;
	[SerializeField] private Transform effectPoint;
	[SerializeField] private Transform leftLightingPoint;
	[SerializeField] private Transform rightLightingPoint;

	public GameObject[] effectPrefabs;
	public GameObject meoteorPrefab;
	public GameObject lightningPrefab;

	private GameObject platform;
	#endregion DataMembers

	public override void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		currentComboStrikes = 1;
		direction = Vector2.right;
		maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		platform = GameObject.Find("BossPlatform");
		//healthBar_slider = GameSession.instance.GetBossHealthBar();
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
		Debug.Log(currentComboStrikes);
		if(!player.GetComponent<PlayerController>().isAlive)
		{
			animator.Play("Taunt");
		}

		if (isCoroutineRunning) return;

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

	}

	//Normal Attack
	private IEnumerator Combo1()
	{
		platform.SetActive(true);
		for(int i = 0; i < Random.Range(1, 4); i++)
		{
			animator.Play("Attack");
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .3f);
			animator.Play("Idle");
			yield return new WaitForSeconds(2f);
		}

		RandomComboStrike();
		platform.SetActive(false);
		isCoroutineRunning = false;
	}

	public IEnumerator SpawnEffect()
	{
		Vector3 spawnPoint = effectPoint.position;
		for(int i = 0; i < effectPrefabs.Length; i++)
		{
			var go = Instantiate(effectPrefabs[i], spawnPoint, Quaternion.identity);
			Destroy(go,.3f);
			spawnPoint += new Vector3(-4, 0, 0);
			yield return new WaitForSeconds(.2f);
		}

	}

	IEnumerator PlayBallSound()
	{
		PlaySound("PriestBall");
		yield return new WaitForSeconds(1f);
		StopSound("PriestBall");
	}


	private IEnumerator Combo2()
	{
		Camera cam = Camera.main;

		animator.Play("SpecialAttack");
		yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length + .1f);
		animator.Play("Laugh");
		FindObjectOfType<SoundManager>().PlayAudio("GiantGirl_Laugh");
		Vector3 leftBound = cam.ViewportToWorldPoint(new Vector3(0, .8f, cam.nearClipPlane));
		Vector3 rightBound = cam.ViewportToWorldPoint(new Vector3(1, .8f, cam.nearClipPlane));
		FindObjectOfType<SoundManager>().PlayAudio("Rock_Falling");
		for (int i = 0; i < Random.Range(1, 10); i++)
		{
			// Tạo một vị trí spawn ngẫu nhiên giữa cạnh trái và cạnh phải của màn hình
			Vector3 spawnLocation = new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y, 0);
			var go = Instantiate(meoteorPrefab, spawnLocation, Quaternion.identity);
			spawnLocation = new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y, 0);
			var go2 = Instantiate(meoteorPrefab, spawnLocation, Quaternion.identity);
			spawnLocation = new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y, 0);
			var go3 = Instantiate(meoteorPrefab, spawnLocation, Quaternion.identity);
			spawnLocation = new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y, 0);
			var go4 = Instantiate(meoteorPrefab, spawnLocation, Quaternion.identity);

			Destroy(go, 2f);
			Destroy(go2, 2f);
			Destroy(go3, 2f);
			Destroy(go4, 2f);

			yield return new WaitForSeconds(1.5f);
		}

		RandomComboStrike();
		isCoroutineRunning = false;
		yield return null;
	}
	
	//Lan xuong hoi mau
	private IEnumerator Combo3()
	{
		animator.Play("Dive");
		GetComponent<Collider2D>().enabled = false;
		for(int i = 0; i < Random.Range(4, 6); i++)
		{
			currentHealth += Random.Range(13, 17);
			healthBar_slider.value = currentHealth;
			yield return new WaitForSeconds(1f);
		}
		animator.Play("GetUp");
		yield return new WaitForSeconds(2f);
		GetComponent<Collider2D>().enabled = true;
		RandomComboStrike();
		isCoroutineRunning = false;
	}

	//Trieu hoi tuong set
	private IEnumerator Combo4()
	{
		StartCoroutine(PlayBallSound());
		animator.Play("Laugh");
		FindObjectOfType<SoundManager>().PlayAudio("GiantGirl_Laugh");

		Camera cam = Camera.main;
		float topY = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane)).y;

		// Tạo effect tại vị trí x của leftPoint
		Vector3 spawnPositionLeft = new Vector3(leftLightingPoint.position.x, topY, 0);
		var go = Instantiate(lightningPrefab, spawnPositionLeft, Quaternion.identity);
		var go2 = Instantiate(lightningPrefab, spawnPositionLeft + new Vector3(2,0,0), Quaternion.identity);

		// Tạo effect tại vị trí x của rightPoint
		Vector3 spawnPositionRight = new Vector3(rightLightingPoint.position.x, topY, 0);
		var go3 = Instantiate(lightningPrefab, spawnPositionRight, Quaternion.identity);
		var go4 = Instantiate(lightningPrefab, spawnPositionRight + new Vector3(-2, 0, 0), Quaternion.identity);

		yield return new WaitForSeconds(5f);

		animator.Play("Idle");
		Destroy(go);
		Destroy(go2);
		Destroy(go3);
		Destroy(go4);

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
			Instantiate(coinPrefab, transform.position + new Vector3(Random.Range(-6, -3), 0, 0), transform.rotation);
		}
	}

	public override bool Die()
	{
		Equipment equipment = GameSession.instance.GetEquipment();
		equipment.IncreaseSlot();
		isAlive = false;
		this.enabled = false;
		GameObject.Find("BlockDoorEffect").SetActive(false);
		healthBar_slider.transform.parent.gameObject.SetActive(false);
		StartCoroutine(DieDelay());
		GameSession.instance.gameObject.SetActive(false);
		SceneManager.LoadScene("EndOfDemo");
		return true;
	}

}
