using System;
using UnityEngine;

public class Health : MonoBehaviour
{

	[SerializeField] public int CurrentHealth;
    [SerializeField] private int maxHealth;
    
    void Start()
    {
        CurrentHealth = maxHealth;
	}

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
		}
	}

    public void GetHealth(int health) 
    {
        this.CurrentHealth += health;

        if (this.CurrentHealth > maxHealth)
        {
            this.CurrentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        this.CurrentHealth -= damage;
    }

    private void Die()
    {
        var objectHealth = GetComponent<IHealth>();

        if (objectHealth != null)
        {
            objectHealth.Die();
        }
        else
        {
            Destroy(gameObject);
        }
    } 
}
