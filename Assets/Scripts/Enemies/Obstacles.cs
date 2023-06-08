using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] protected float obstacleHealth;
    [SerializeField] protected float obstacleSpeed;

    void Update()
    {
        if (obstacleHealth <= 0)
        {
            ObstacleDeath();
        }
    }

    public void ObstacleTakeDamage(float damage)
    {
        obstacleHealth -= damage;
    }

    public void ObstacleDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            player.TakeDamage(10);
            player.TakePoints(10);
            Destroy(gameObject);
        } 
    }
}
