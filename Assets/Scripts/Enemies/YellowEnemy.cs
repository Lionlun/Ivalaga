using UnityEngine;

public class YellowEnemy : EnemyBaseClass, IHealth
{
	#region Attack
	[SerializeField] private EnemyBulletBehaviour enemyBullet;
	[SerializeField] private Transform shootingPoint;
	float timerForNextAttack;
	float cooldown;
	#endregion

	[SerializeField] private ParticleSystem particlePrefab;
    Animator animator;

	void Start()
    {
		animator = GetComponent<Animator>();
        cooldown = 1;
        timerForNextAttack = cooldown;
    }
        
    void Update()
    {
        EnemyAttack();
    }

	protected override void EnemyAttack()
	{
		if (timerForNextAttack > 0)
		{
			timerForNextAttack -= Time.deltaTime;
		}
		else if (timerForNextAttack <= 0)
		{
			animator.SetTrigger("Recoil");
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);

			timerForNextAttack = cooldown;
		}
	}

	public override void Die()
	{
		var particleEffect = Instantiate(particlePrefab, transform.position, Quaternion.identity);
		particleEffect.Play();
		GlobalEvents.SendEnemyKilled();
		Destroy(gameObject);
	}
}

   

