using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class BossDiamondBehaviour : IBossBehaviour
{
	float timerForNextAttack;
	float cooldown = 4f;
	float speed = 0.34f;

	Transform shootingPoint;
	BossRay bossRay;
	Boss boss;
	ParticleSystem teleportEffectPrefab;

	Vector3 teleportTempPosition;
	Vector3 bossCenterPosition = new Vector3(0, 3);

	public override void Enter()
	{
		timerForNextAttack = cooldown;
		Debug.Log("Enter Circle Behaviour");
	}

	public override void Exit()
	{
		var effect = Instantiate(teleportEffectPrefab, boss.transform.position, Quaternion.identity);
		teleportEffectPrefab.Play();
	}
	public override void Update()
	{
		Debug.Log("Update Circle Behaviour");
	}

	public override void Move()
	{
			if (boss.IsMovingRight)
			{
				boss.transform.position += Vector3.right * speed;
			}
			if (boss.IsMovingLeft)
			{
				boss.transform.position += Vector3.left * speed;
			}
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

	public void Init(Boss boss, BossRay bossRay, Transform shootingPoint, ParticleSystem teleportEffectPrefab)
	{
		this.boss = boss;
		this.bossRay = bossRay;
		this.shootingPoint = shootingPoint;
		this.teleportEffectPrefab = teleportEffectPrefab;
	}

	public void Teleport()
	{
		while(teleportTempPosition != bossCenterPosition)
		{
			var newTeleportPrefab = Instantiate(teleportEffectPrefab, teleportTempPosition, Quaternion.identity);
			teleportTempPosition = Vector3.MoveTowards(newTeleportPrefab.transform.position, bossCenterPosition, 0.1f);
		}
	}
}
