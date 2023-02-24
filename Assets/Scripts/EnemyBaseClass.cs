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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

  
    protected abstract void EnemyAttack(float damage);

    public abstract void EnemyTakeDamage(float damage);

    public void EnemyDeath()
    {
       
            Debug.Log("EnemyDead");
            Destroy(gameObject);
        
    }
}
