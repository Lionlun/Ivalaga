using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnemyManager : MonoBehaviour
{
    int numberOfEnemiesTotal = 0;
    List<EnemyController> enemies;
    private int numberOfEnemiesRemaining;

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
