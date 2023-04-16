using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBaseClass firstenemyPrefab;
    [SerializeField] private GameObject squadFirstEnemy;
    [SerializeField] private EnemyBaseClass boss;
    [SerializeField] private Octagon octagon;
 
    private float firstEnemyInterval = 9f;

    private float timeToSpawnBoss = 150f;
    private float timeToSpawnOctagon = 75f;
    [SerializeField]private Transform spawnPosition;
    private Vector2 octagonSpawn;

    void Start()
    {
        octagonSpawn = new Vector2(20, Random.Range(14, 5));
        StartCoroutine(SpawnEnemy(firstEnemyInterval, firstenemyPrefab));
        StartCoroutine(SpawnBoss(timeToSpawnBoss, boss));
		StartCoroutine(SpawnOctagon(timeToSpawnOctagon, octagon));
	}

   private IEnumerator SpawnEnemy(float interval, EnemyBaseClass enemy)
    {
        yield return new WaitForSeconds(interval);
        EnemyBaseClass newEnemy = Instantiate(enemy, spawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(interval);
        GameObject newSquad = Instantiate(squadFirstEnemy, spawnPosition.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    private IEnumerator SpawnBoss(float interval, EnemyBaseClass boss)
    {
        yield return new WaitForSeconds(interval);
        EnemyBaseClass newBoss = Instantiate(boss, spawnPosition.position, Quaternion.identity);
    }

    private IEnumerator SpawnOctagon(float interval, Octagon octagon)
    {
        yield return new WaitForSeconds(interval);
        Octagon newOctagon = Instantiate(octagon, octagonSpawn, Quaternion.identity);
	}
}
