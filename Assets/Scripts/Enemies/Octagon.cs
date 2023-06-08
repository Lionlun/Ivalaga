using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class Octagon : EnemyBaseClass
{
	[SerializeField] private OctagonBulletFormation bullet;
	[SerializeField] private Transform shootingPoint;

	[SerializeField] private OctagonBulletFormation octagonBulletFormation;

	Animator animator;

	private Vector3 rotationSpeed = new Vector3(0,0,1);

	float AttackTiming = 4f;
	float cooldown = 0.1f;
	float timeToDeath = 16f;

	void Start()
    {
		animator = GetComponent<Animator>();
	}

    void Update()
    {
		Rotation();
		SelfDestruction();
		EnemyAttack();
	}

	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
	}

	protected override void EnemyAttack()
	{
		if (AttackTiming > 0)
		{
			AttackTiming -= Time.deltaTime;
		}
		else if (AttackTiming <= 0)
		{
			var newBulletFormation = Instantiate(octagonBulletFormation, shootingPoint.position, Quaternion.Euler(rotationSpeed));
			
			AttackTiming = cooldown;
		}
	}

	private void Rotation()
	{
		rotationSpeed += new Vector3(0, 0, 1);
		transform.rotation = Quaternion.Euler(rotationSpeed);
	}

	private void SelfDestruction()
	{
		if (timeToDeath > 0)
		{
			timeToDeath -= Time.deltaTime;
		}

		else
		{
			animator.SetTrigger("IsDead");
		}
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
