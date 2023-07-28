using UnityEngine;

public class PlayerBullet : PlayerBulletBase
{
	[SerializeField] int bulletDamage = 10;

	private void PlayEffect()
	{
		var particleEffect = Instantiate(ParticleEffectPrefab, transform.position, Quaternion.identity);
		particleEffect.transform.position = transform.position;
		particleEffect.Play();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			PlayEffect();

			EnemyBaseClass enemy = collision.gameObject.GetComponentInParent<EnemyBaseClass>();
			Health enemyHealth = collision.gameObject.GetComponentInParent<Health>();

			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(bulletDamage);
			}

			Destroy(gameObject);
		}
	}
}
