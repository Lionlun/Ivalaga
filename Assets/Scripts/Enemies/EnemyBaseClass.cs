using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBaseClass : MonoBehaviour
{
    #region EnemyPropert

    [SerializeField] protected float enemyHealth;
    [SerializeField] protected float enemySpeed;
    #endregion
     
    void Update()
    {
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

  
    protected abstract void EnemyAttack();

    public abstract void EnemyTakeDamage(float damage);

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
