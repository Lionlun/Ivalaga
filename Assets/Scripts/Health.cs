using System;
using UnityEngine;

public class Health : MonoBehaviour
{

	[SerializeField] private int health;
    [SerializeField] private int maxHealth;
    

    void Start()
    {
        health = maxHealth;
	}

    void Update()
    {
        if (health <= 0)
        {
            Die();
		}
	}

    public void GetHealth(int health) 
    {
        this.health += health;

        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
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
