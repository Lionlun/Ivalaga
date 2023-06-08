using UnityEngine;

public class Bullet2 : PlayerBulletBase
{
	public int bulletDamage = 20;

	private void Awake()
	{
		BulletOwnDamage = 2;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			var particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
			particleEffect.transform.position = transform.position;
			particleEffect.Play();

			EnemyBaseClass enemy = collision.gameObject.GetComponentInParent<EnemyBaseClass>();


			if (enemy != null)
			{
				enemy.EnemyTakeDamage(bulletDamage);
			}

			Destroy(gameObject);
		}
	}

}
