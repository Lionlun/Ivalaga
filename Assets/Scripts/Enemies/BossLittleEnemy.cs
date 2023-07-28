using UnityEngine;


public class BossLittleEnemy : EnemyBaseClass
{
	#region Attack
	private float cooldown = 8f;
	private float timerForNextAttack;
	private float timerToStartAttack = 5f;
	[SerializeField] Transform shootingPoint;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
	#endregion

	public Boss Boss;

	private Vector3 offset;
	
	private float timeToTransform = 5f;
	
	[SerializeField] BossLittleEnemyTier2 littleEnemyTier2;
	[SerializeField] ParticleSystem transformEffect;

	void Start()
    {
		timerForNextAttack = cooldown;
		Boss = FindObjectOfType<Boss>();
	}

	void Update()
	{
		EnemyAttack();
	}

	private void OnEnable()
	{
		Boss.OnBossDestroyed += Die;
	}

	private void OnDisable()
	{
		Boss.OnBossDestroyed -= Die;
	}

	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
	}

	protected override void EnemyAttack()
	{
		if(timerToStartAttack > 0)
		{
			timerToStartAttack -= Time.deltaTime;
		}
		else
		{
			if (timerForNextAttack > 0)
			{
				timerForNextAttack -= Time.deltaTime;
			}
			else if (timerForNextAttack <= 0)
			{
				Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);

				timerForNextAttack = cooldown;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponent<BossLittleEnemy>())
		{
			if (timeToTransform >= 0)
			{
				timeToTransform -= Time.deltaTime;
			}
			else
			{
				timeToTransform = 5f;

				var tier2 = Instantiate(littleEnemyTier2, transform.position, Quaternion.identity);
				
				tier2.SetOffset(offset);
				var effect = Instantiate(transformEffect, transform.position, Quaternion.identity);
				effect.Play();
				this.gameObject.SetActive(false);
			}
		}

		if (collision.GetComponentInParent<CharacterController2D>())
		{
			var player = collision.GetComponentInParent<CharacterController2D>();
			var pushBackVector = transform.position - player.transform.position;
			player.transform.position = Vector3.MoveTowards(player.transform.position, pushBackVector*2, 0.1f);
		}
	}
}
