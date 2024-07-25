using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHitBox : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();

		if (enemy)
		{
			StartCoroutine(ApplyDamage(enemy));
		}
	}

	IEnumerator ApplyDamage(Enemy enemy)
	{
		int comboPhase = 4;
		int currentPhase = 0;

		while(currentPhase < comboPhase)
		{
			enemy.TakeDamage(20);
			yield return new WaitForSeconds(.3f);
			currentPhase++;
		}

		
	}
}
