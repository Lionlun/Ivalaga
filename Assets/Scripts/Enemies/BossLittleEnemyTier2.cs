using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLittleEnemyTier2 : EnemyBaseClass
{
	public Boss Boss;
	private Vector3 offset;
	private float timerForNextAttack;
	private float cooldown = 3f;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
	[SerializeField] Transform shootingPoint;
	
	void Start()
	{
		timerForNextAttack = cooldown;
		Boss = FindObjectOfType<Boss>();
	}
	void Update()
	{
		EnemyAttack();
		if (EnemyHealth<= 0) 
		{
			Die();
		}
		Move();
	}
	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
	}

	protected override void EnemyAttack()
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
	private void Move()
	{
		if (Boss != null)
		{
			var locationNeeded = new Vector2 (Boss.transform.position.x+offset.x, Boss.transform.position.y+offset.y);
			transform.position = Vector3.MoveTowards(transform.position, locationNeeded, 12f * Time.deltaTime);
		}
	}

	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.GetComponentInParent<CharacterController2D>())
		{
			Debug.Log("PLAYER PUSHED");
			var player = collision.GetComponentInParent<CharacterController2D>();
			var pushBackVector = transform.position - player.transform.position;
			player.transform.position = Vector3.MoveTowards(player.transform.position, pushBackVector * 2, 0.1f);
		}
	}

}
