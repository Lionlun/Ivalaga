using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] protected float ObstacleHealth;
    [SerializeField] protected float ObstacleSpeed;

    void Update()
    {
        if (ObstacleHealth <= 0)
        {
            ObstacleDeath();
        }
    }

    public void ObstacleTakeDamage(float damage)
    {
        ObstacleHealth -= damage;
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
			Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
			playerHealth.TakeDamage(10);
            player.TakePoints(10);
            Destroy(gameObject);
        } 
    }
}
