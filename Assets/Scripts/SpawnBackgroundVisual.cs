using System.Collections;
using UnityEngine;

public class SpawnBackgroundVisual : MonoBehaviour
{
    [SerializeField] BackGroundSnake snakePrefab;

    private float snakeRespawnTime =5f;
    private float snakeGap = 0.2f;

    void Start()
    {
        StartCoroutine(SpawnSnake(snakePrefab, snakeRespawnTime));
    }

    private IEnumerator SpawnSnake(BackGroundSnake snake, float interval)
    {
		yield return new WaitForSeconds(interval);

		Vector2 snakeSpawnPosition = new Vector2(Random.Range(-13, 13), -10);

        for (int i = 0; i < 8; i++)
        {
			var newSnake = Instantiate(snake, snakeSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(snakeGap);
		}
      
        StartCoroutine(SpawnSnake(snake, interval));
    }
}
