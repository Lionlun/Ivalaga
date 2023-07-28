using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
	int DamageToDie = 400;

	private void OnEnable()
	{
		Boss.OnCreated += DestroyEnemies;
	}
	private void OnDisable()
	{
		Boss.OnCreated -= DestroyEnemies;
	}
	void DestroyEnemies()
	{
		var blueEnemies = FindObjectsOfType(typeof(BlueEnemy));

		var yellowEnemies = FindObjectsOfType(typeof(YellowEnemy));

		foreach (BlueEnemy obj in blueEnemies)
		{
			var objectHealth = obj.gameObject.GetComponent<Health>();
			objectHealth.TakeDamage(DamageToDie);
		}
	
		foreach (YellowEnemy obj in yellowEnemies)
		{
			var objectHealth = obj.gameObject.GetComponent<Health>();
			objectHealth.TakeDamage(DamageToDie);
		}
	}

}
