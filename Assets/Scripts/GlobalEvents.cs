using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent OnHealthPackPickUp = new UnityEvent(); //не уверен, что должен быть глобальным
     
    
    public static void SendHealthPackPickedUp()
    {
        OnHealthPackPickUp.Invoke();
    }

    public static void SendEnemyKilled()
    {
        OnEnemyKilled.Invoke();
    }
}
