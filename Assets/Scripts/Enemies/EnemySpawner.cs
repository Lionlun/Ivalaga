using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	#region Prefabs
	[SerializeField] private EnemyBaseClass yellowEnemy;
	[SerializeField] private GameObject squadFirstEnemy;
	[SerializeField] private Boss boss;
	[SerializeField] private Octagon octagon;
	[SerializeField] private GreatRay greatRay;
	[SerializeField] private BlueEnemy blueEnemy;
	[SerializeField] private Player player;
	[SerializeField] private HorizontalProjectile horizontalProjectile;
	#endregion

	#region SpawnTime
	private float firstEnemyInterval = 9f;
	private float timeToSpawnBoss = 150f; //150
	private float timeToSpawnOctagon = 75f; //75
	private float timeToSpawnGreatRay = 30f;
	private float timeToSpawnHorizontalProjectile = 5f;
	private float timeToSpawnBlueEnemy = 30f;
	#endregion

	#region SpawnPosition
	[SerializeField] private Transform spawnPosition;
	private Vector2 octagonSpawn;
	private Vector2 blueEnemySpawn = new Vector2(-16, 7);
	private Vector2 horizontalProjectileSpawn;
	private Vector2 bossSpawnPosition = new Vector2(0, 20);
	#endregion

	void Start()
    {
		octagonSpawn = new Vector2(20, Random.Range(14, 5));
        StartCoroutine(SpawnEnemy(firstEnemyInterval, yellowEnemy));
        StartCoroutine(SpawnBoss(timeToSpawnBoss, boss));
		StartCoroutine(SpawnOctagon(timeToSpawnOctagon, octagon));
       // StartCoroutine(SpawnGreatRay(timeToSpawnGreatRay, greatRay));
        StartCoroutine(SpawnBlueEnemy(timeToSpawnBlueEnemy, blueEnemy));
        StartCoroutine(SpawnHorizontalProjectile(timeToSpawnHorizontalProjectile, horizontalProjectile));
	}
	private void OnEnable()
	{
		Boss.OnCreated += Disable;
	}
	private void OnDisable()
	{
		Boss.OnCreated -= Disable;
	}
	void Disable()
    {
        gameObject.SetActive(false);
    }
   private IEnumerator SpawnEnemy(float interval, EnemyBaseClass enemy)
    {
        yield return new WaitForSeconds(interval);
        EnemyBaseClass newEnemy = Instantiate(enemy, spawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(interval);
        GameObject newSquad = Instantiate(squadFirstEnemy, spawnPosition.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    private IEnumerator SpawnBoss(float interval, Boss boss)
    {
        yield return new WaitForSeconds(interval);
        Boss newBoss = Instantiate(boss, bossSpawnPosition, Quaternion.identity);
    }

    private IEnumerator SpawnOctagon(float interval, Octagon octagon)
    {
        yield return new WaitForSeconds(interval);
        Octagon newOctagon = Instantiate(octagon, octagonSpawn, Quaternion.identity);
	}

    private IEnumerator SpawnGreatRay(float interval, GreatRay greatRay)
    {
		yield return new WaitForSeconds(interval);
        GreatRay newGreatRay = Instantiate(greatRay, new Vector2(player.transform.position.x, 0), Quaternion.identity);
        StartCoroutine(SpawnGreatRay(interval, greatRay));
	}

    private IEnumerator SpawnBlueEnemy(float interval, BlueEnemy blueEnemy)
    {
		yield return new WaitForSeconds(interval);
        for (int i = 0; i <3; i++)
        {
            BlueEnemy newBlueEnemy = Instantiate(blueEnemy, blueEnemySpawn, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
		}

        StartCoroutine(SpawnBlueEnemy(interval, blueEnemy));
	}

    private IEnumerator SpawnHorizontalProjectile(float interval, HorizontalProjectile horizontalProjectile)
    {
		horizontalProjectileSpawn = new Vector2(-45, Random.Range(-6, 8));
		yield return new WaitForSeconds(interval);
		HorizontalProjectile newHorizontalProjectile = Instantiate(horizontalProjectile, horizontalProjectileSpawn, Quaternion.identity);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(SpawnHorizontalProjectile(interval, horizontalProjectile));
	}
}
