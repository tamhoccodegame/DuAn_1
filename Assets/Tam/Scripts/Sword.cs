using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Equipment equipment;

	[SerializeField] private float damage;
	[SerializeField] private ParticleSystem fireEffect;
	[SerializeField] private GameObject electricEffect;
	[SerializeField] private ParticleSystem iceEffect;

	[SerializeField] private bool isFireBuff = false;
	[SerializeField] private bool isIceBuff = false;
	[SerializeField] private bool isElectricBuff = false;
	[SerializeField] private bool isLifestealBuff = false;
	// Start is called before the first frame update
	void Start()
    {
		
    }

	public void SetDamage(float damage)
	{
		this.damage = damage;
	}

	public void AddDamage(float damage)
	{
		this.damage += damage;
	}

	public void SetEquipment(Equipment _equipment)
	{
		equipment = _equipment;
		equipment.OnEquipmentChange += Equipment_OnEquipmentChange;
		RefreshSwordBuff();
	}

	private void Equipment_OnEquipmentChange(object sender, System.EventArgs e)
	{
		RefreshSwordBuff();
	}

	private void RefreshSwordBuff()
	{
		isFireBuff = false;
		isIceBuff = false;
		isElectricBuff = false;
		isLifestealBuff = false;

		foreach (Rune rune in equipment.GetEquipmentList())
		{
			switch (rune.runeType)
			{
				case Rune.RuneType.Fire:
					isFireBuff = true;
					break;
				case Rune.RuneType.Electric:
					isElectricBuff = true;
					break;
				case Rune.RuneType.Ice:
					isIceBuff = true;
					break;
				case Rune.RuneType.Lifesteal:
					isLifestealBuff = true;
					break;

			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("trigger with " + collision.name);
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			GetComponentInParent<Skill_Mana>().AddSkillMana(5);
			FindObjectOfType<SoundManager>().PlayAudio("HitImpact_" + Random.Range(1, 3));
			enemy.TakeDamage(damage);


			if (isFireBuff)
			{
				Instantiate(fireEffect, enemy.transform.position, Quaternion.identity, enemy.transform);
			}


			if (isElectricBuff)
			{
				Enemy[] enemies = FindObjectsOfType<Enemy>();
				Enemy currentEnemy = enemy;
				foreach(Enemy e in enemies)
				{
					float offsetLocationX = (e.transform.position.x - currentEnemy.transform.position.x)/2;
					float scaleY = Vector2.Distance(currentEnemy.transform.position, e.transform.position) + 1;
					Vector3 spawnLocation = new Vector3(currentEnemy.transform.position.x + offsetLocationX, currentEnemy.transform.position.y, 0);

					if (IsEnemyInCameraView(e))
					{
						var eEffect = Instantiate(electricEffect, spawnLocation + new Vector3(0, 1, 0), Quaternion.identity);
						eEffect.transform.rotation = Quaternion.Euler(0, 0, -90f);
						eEffect.transform.localScale = new Vector3(eEffect.transform.localScale.x,
																   scaleY,
																   eEffect.transform.localScale.z
																  );
						Destroy(eEffect, .2f);
					}
					currentEnemy = e;
				}
			}
		}
	}

	bool IsEnemyInCameraView(Enemy enemy)
	{
		Camera mainCamera = Camera.main;
		// Lấy vị trí kẻ địch trong thế giới
		Vector3 enemyPosition = enemy.transform.position;

		// Chuyển đổi vị trí kẻ địch từ thế giới sang viewport (0-1)
		Vector3 viewportPosition = mainCamera.WorldToViewportPoint(enemyPosition);

		// Kiểm tra xem kẻ địch có nằm trong khung nhìn của camera không
		return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
			   viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
			   viewportPosition.z > 0; // Đảm bảo kẻ địch không nằm sau camera
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
