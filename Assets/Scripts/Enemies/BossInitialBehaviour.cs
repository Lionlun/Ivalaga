using UnityEngine;

[CreateAssetMenu]
public class BossInitialBehaviour : IBossBehaviour
{
	#region Attack
	private float timerForNextAttack;
	private float cooldown = 0.23f;
	EnemyBulletBehaviour enemyBullet;
	Transform shootingPoint;
	#endregion

	private Boss boss;
	private BossMover bossMover;
	ParticleSystem shootEffect;

	public override void Enter()
	{
		timerForNextAttack = cooldown;
	}

	public override void Exit()
	{

	}

	public override void Update()
	{
		Attack();

	}

	public override void Attack()
	{
		if (timerForNextAttack > 0)
		{
			timerForNextAttack -= Time.deltaTime;
		}
		else if (timerForNextAttack < 0)
		{
			var effect = Instantiate(shootEffect, shootingPoint.position, Quaternion.identity);
			effect.Play();
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);
			timerForNextAttack = cooldown;
		}
	}

	public void Init(Boss boss, BossMover bossMover, EnemyBulletBehaviour enemyBullet, Transform shootingPoint, ParticleSystem shootEffect)
	{
		this.boss = boss;
		this.bossMover = bossMover;
		this.enemyBullet = enemyBullet;
		this.shootingPoint = shootingPoint;
		this.shootEffect = shootEffect;
	}
}
