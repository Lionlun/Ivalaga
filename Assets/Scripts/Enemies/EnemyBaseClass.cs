using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] protected float EnemyHealth;
  
    protected abstract void EnemyAttack();

    public abstract void EnemyTakeDamage(float damage);

    public virtual void Die()
    {
        Destroy(gameObject);
    }

}
