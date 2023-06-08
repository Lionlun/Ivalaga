using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using static UnityEditor.Rendering.FilterWindow;
using System.Runtime.CompilerServices;
using System;

public class BossLittleEnemy : EnemyBaseClass
{
	public Boss Boss;
	private Vector3 offset;
	private float cooldown = 8f;
	private float timerForNextAttack;

	private float timeToTransform = 5f;
	private float timerToStartAttack = 5f;
	[SerializeField] Transform shootingPoint;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
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
	
		if (EnemyHealth <= 0)
		{
			Die();
		}
	}

	private void OnEnable()
	{
		Boss.OnBossDestroyed += Die;
	}

	private void OnDisable()
	{
		Boss.OnBossDestroyed -= Die;
	}
	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
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
				Debug.Log("LittleShoot");
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
