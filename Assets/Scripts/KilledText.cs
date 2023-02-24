using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KilledText : MonoBehaviour
{
    private int killed = 0;
    

    void Awake()
    {
        GlobalEvents.OnEnemyKilled.AddListener(EnemyKilled);

    }

    // Update is called once per frame
    void EnemyKilled()
    {
        killed++;
        GetComponent<TMP_Text>().text = "Killed: " + killed;
 
    }

    


}
