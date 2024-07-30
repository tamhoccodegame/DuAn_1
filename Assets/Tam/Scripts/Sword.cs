using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Equipment equipment;

	[SerializeField] private float damage;
	[SerializeField] private ParticleSystem fireEffect;
	[SerializeField] private ParticleSystem electricEffect;
	[SerializeField] private ParticleSystem iceEffect;

	[SerializeField] private bool isFireBuff = false;
	[SerializeField] private bool isIceBuff = false;
	[SerializeField] private bool isElectricBuff = false;
	[SerializeField] private bool isLifestealBuff = false;
	// Start is called before the first frame update
	void Start()
    {
		
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
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			Destroy(enemy.transform.Find("BossRage")?.gameObject);
			enemy.TakeDamage(damage);
			Debug.Log(isFireBuff);
			if (isFireBuff)
			{
				Instantiate(fireEffect, enemy.transform.position, Quaternion.identity, enemy.transform);
			} 
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
