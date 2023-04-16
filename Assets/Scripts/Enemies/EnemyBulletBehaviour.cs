using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private float enemyBulletSpeed = 10f;

    void Update()
    {
        BulletMovement();
        Destroy(gameObject, 2);
    }

    void BulletMovement()
    {
        transform.Translate(Vector3.down * enemyBulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            player.OwnDamage(10);
            player.TakePoints(10);
			Destroy(gameObject);
		}
       
    }
}
