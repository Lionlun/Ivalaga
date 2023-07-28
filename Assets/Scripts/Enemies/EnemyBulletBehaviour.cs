using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float enemyBulletSpeed = 10f;
    [SerializeField] private float lifetime = 2;

	[SerializeField] ParticleSystem particleEffect;

	void Update()
    {
        BulletMovement();
        Destroy(gameObject, lifetime);
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
			Health playerHealth = collision.gameObject.GetComponentInParent<Health>();

			playerHealth.TakeDamage(10);
            player.TakePoints(50);

			var effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
			effect.Play();

			Destroy(gameObject);
		}
    }
}
