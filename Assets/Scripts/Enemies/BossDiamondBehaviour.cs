using UnityEngine;

[CreateAssetMenu]
public class BossDiamondBehaviour : IBossBehaviour
{
	#region Attack
	float timerForNextAttack;
	float cooldown = 4f;
	Transform shootingPoint;
	BossRay bossRay;
	#endregion

	Boss boss;
	ParticleSystem teleportEffectPrefab;

	public override void Enter()
	{
		timerForNextAttack = cooldown;
	}

	public override void Exit()
	{
		var effect = Instantiate(teleportEffectPrefab, boss.transform.position, Quaternion.identity);
		teleportEffectPrefab.Play();
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
			Instantiate(bossRay, shootingPoint.position, Quaternion.identity);
			
			timerForNextAttack = cooldown;
		}
	}

	public void Init(Boss boss, BossMover bossMover, BossRay bossRay, Transform shootingPoint, ParticleSystem teleportEffectPrefab)
	{
		this.boss = boss;
		this.bossRay = bossRay;
		this.shootingPoint = shootingPoint;
		this.teleportEffectPrefab = teleportEffectPrefab;
	}
}
