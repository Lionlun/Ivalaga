using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [SerializeField] GameObject healthPrefab;

    [SerializeField] float secondSpawn = 18f;
	[SerializeField] float secondSpawnDuringBoss = 7f;

	[SerializeField] float minTras;
    [SerializeField] float maxTras;
	private bool isBossSpawned;

	void Start()
    {
        StartCoroutine(HealthSpawn());
    }

	private void OnEnable()
	{
		Boss.OnCreated += SetBossTrue;
        Boss.OnCreated += SpawnHealOnBoss;
	}

	private void OnDisable()
	{
		Boss.OnCreated -= SetBossTrue;
		Boss.OnCreated -= SpawnHealOnBoss;
	}
	IEnumerator HealthSpawn()
    {
        while (!isBossSpawned)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(healthPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 10f);
        }
        while (isBossSpawned)
        {
			var wanted = Random.Range(minTras, maxTras);
			var position = new Vector3(wanted, transform.position.y);
			GameObject gameObject = Instantiate(healthPrefab, position, Quaternion.identity);
			yield return new WaitForSeconds(secondSpawnDuringBoss);
			Destroy(gameObject, 10f);
		}   
    }

    public void SetBossTrue()
    {
        isBossSpawned = true;
    }

    public void SpawnHealOnBoss()
    {
        for (int i =0; i< 3; i++)
        {
			var wanted = Random.Range(minTras, maxTras);
			var position = new Vector3(wanted, transform.position.y);
			GameObject gameObject = Instantiate(healthPrefab, position, Quaternion.identity);
			Destroy(gameObject, 10f);
		}
    }
}
