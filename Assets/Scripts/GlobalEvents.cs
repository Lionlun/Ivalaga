using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static UnityEvent OnEnemyKilled = new UnityEvent();
     
    public static void SendEnemyKilled()
    {
        OnEnemyKilled?.Invoke();
    }
}
