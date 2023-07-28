using System.Collections;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
	[SerializeField] HorizontalProjectile horizontalProjectile;
	float timeToSpawnHorizontalProjectile = 2f;
	Vector2 horizontalProjectileSpawn;

	void OnEnable()
	{
		Boss.OnCreated += StartAllCoroutines;
	}
	void OnDisable()
	{
		Boss.OnCreated -= StartAllCoroutines;
	}

	void StartAllCoroutines()
	{
			StartCoroutine(SpawnHorizontalProjectile(timeToSpawnHorizontalProjectile, horizontalProjectile));
	}

	IEnumerator SpawnHorizontalProjectile(float interval, HorizontalProjectile horizontalProjectile)
	{
		horizontalProjectileSpawn = new Vector2(-20, Random.Range(-6, 8));
		yield return new WaitForSeconds(interval);
		HorizontalProjectile newHorizontalProjectile = Instantiate(horizontalProjectile, horizontalProjectileSpawn, Quaternion.identity);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(SpawnHorizontalProjectile(interval, horizontalProjectile));
	}
}
