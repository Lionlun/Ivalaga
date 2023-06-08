using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BossInitialBehaviour : IBossBehaviour
{
	private float speed = 0.2f;
	private float timerForNextAttack;
	private float cooldown = 0.23f;
	EnemyBulletBehaviour enemyBullet;
	Transform shootingPoint;
	private Boss boss;
	public override void Enter()
	{
		timerForNextAttack = cooldown;
		Debug.Log("Enter Initial Behaviour");
	}

	public override void Exit()
	{
		Debug.Log("Exit Initial Behaviour");
	}
	public override void Update()
	{
		Debug.Log("Update Initial Behaviour");
	}

	public override void Move()
	{
		if (boss.transform.position.y != 8)
		{
			boss.transform.position = new Vector3(boss.transform.position.x, 8);
		}

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
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);
			timerForNextAttack = cooldown;
		}
	}

	public void Init(Boss boss, EnemyBulletBehaviour enemyBullet, Transform shootingPoint)
	{
		this.boss = boss;
		this.enemyBullet = enemyBullet;
		this.shootingPoint = shootingPoint;
	}
}
