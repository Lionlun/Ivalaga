using UnityEngine;

public class Bullet3 : PlayerBulletBase
{
	public int bulletDamage = 30;

	private void Awake()
	{
		BulletOwnDamage = 3;
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
				Debug.Log("Bullet Damage: " + bulletDamage);
			}

			Destroy(gameObject);
		}
	}

}