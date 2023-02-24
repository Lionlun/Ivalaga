using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBaseClass firstenemyPrefab;
    [SerializeField] private GameObject squadFirstEnemy;
    [SerializeField] private EnemyBaseClass boss;
    //private EnemyBaseClass secondenemyPrefab;
    private float firstEnemyInterval = 3.5f;

    private float timeToSpawnBoss = 20f;
    [SerializeField]private Transform spawnPosition;

    void Start()
    {
        StartCoroutine(SpawnEnemy(firstEnemyInterval, firstenemyPrefab));
        StartCoroutine(SpawnBoss(timeToSpawnBoss, boss));
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
}
