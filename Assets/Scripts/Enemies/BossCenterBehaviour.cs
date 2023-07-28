using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class BossCenterBehaviour : IBossBehaviour
{
	#region Attack
	float timerForNextAttack;
	float cooldown = 0.1f;
	Transform shootingPoint;
	EnemyBulletBehaviour enemyBullet;
	#endregion

	float angle = 0;
	
	Boss boss;

	ParticleSystem teleportEffectPrefab;

	public async override void Enter()
	{
		shootingPoint.position = new Vector3(boss.transform.position.x, boss.transform.position.y-1.5f);
		timerForNextAttack = cooldown;
		await Task.Delay(300);
		var effect = Instantiate(teleportEffectPrefab, boss.transform.position + new Vector3(0,-2), Quaternion.identity);
		effect.Play();
	}

	public override void Exit()
	{
		var effect = Instantiate(teleportEffectPrefab, boss.transform.position, Quaternion.identity);
		effect.Play();
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
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.Euler(0,0,angle));
			angle += 20;
			timerForNextAttack = cooldown;
		}
	}

	public void Init(Boss boss, EnemyBulletBehaviour enemyBullet, Transform shootingPoint, ParticleSystem teleportEffectPrefab)
	{
		this.boss = boss;
		this.enemyBullet = enemyBullet;
		this.shootingPoint = shootingPoint;
		this.teleportEffectPrefab= teleportEffectPrefab;
	}
}
