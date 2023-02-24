using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    //временное решение
    private void OnDestroy()
    {
        GlobalEvents.SendEnemyKilled();
    }
}
