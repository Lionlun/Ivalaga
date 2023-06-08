using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
	[SerializeField] private HorizontalProjectile horizontalProjectile;
	private float timeToSpawnHorizontalProjectile = 2f;
	private Vector2 horizontalProjectileSpawn;

	private void OnEnable()
	{
		Boss.OnCreated += StartAllCoroutines;
	}
	private void OnDisable()
	{
		Boss.OnCreated -= StartAllCoroutines;
	}

	private void StartAllCoroutines()
	{
			StartCoroutine(SpawnHorizontalProjectile(timeToSpawnHorizontalProjectile, horizontalProjectile));
	}

	private IEnumerator SpawnHorizontalProjectile(float interval, HorizontalProjectile horizontalProjectile)
	{
		horizontalProjectileSpawn = new Vector2(-20, Random.Range(-6, 8));
		yield return new WaitForSeconds(interval);
		HorizontalProjectile newHorizontalProjectile = Instantiate(horizontalProjectile, horizontalProjectileSpawn, Quaternion.identity);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(SpawnHorizontalProjectile(interval, horizontalProjectile));
	}
}
