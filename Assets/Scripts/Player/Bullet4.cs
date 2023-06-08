using UnityEngine;

public class Bullet4 : PlayerBulletBase
{
	public int bulletDamage = 40;

	private void Awake()
	{
		BulletOwnDamage = 4;
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
