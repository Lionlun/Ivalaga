using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	int numberOfEnemiesRemaining;
	int numberOfEnemiesTotal = 0;
    List<EnemyController> enemies;

    public void RegisterEnemy(EnemyController enemy)
    {
        enemies.Add(enemy);
        numberOfEnemiesTotal++;
    }

    public void UnregisterEnemy(EnemyController enemyKilled)
    {
        int enemiesRemainingNotification = numberOfEnemiesRemaining - 1;
    }
}
