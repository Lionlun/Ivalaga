using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BossPart: EnemyBaseClass
{
	[SerializeField] public int bossPartNumber;
	Boss boss;
	[SerializeField] BossLittleEnemy enemy;
	[SerializeField] BossPart fractionPrefab;

	Vector3 position;
	public Vector3 circlePosition;

	private Vector3 initialPosition;

	private Vector3 centerPosition;

	public static event Action OnDestroyed;
	public bool isDestroyed;


	void Start()
	{
		EnemyHealth = 200; //200
		boss = GetComponentInParent<Boss>();

		boss.BossParts.Add(this);
	}
	void Update()
	{
		if (EnemyHealth <=0) 
		{
			OnDestroyed?.Invoke();
			InstantiateFractions();
			SpawnEnemyOnDeath();
			boss.BossParts.Remove(this);
			Die();
		}
	}

	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
	}

	public void SetCirclePosition(Vector3 circlePosition)
	{
		this.circlePosition = circlePosition;
	}
	public void SetInitialPosition(Vector3 initialPosition)
	{
		this.initialPosition = initialPosition;
	}
	public void SetCenterPosition(Vector3 centerPosition)
	{
		this.centerPosition = centerPosition;
	}
	protected override void EnemyAttack()
	{
		throw new System.NotImplementedException();
	}

	public async Task MovePartsToCircle()
	{
		if (this.gameObject == null)
		{
			return;
		}
		while (transform.position != circlePosition && this.gameObject != null)
		{
		transform.position = Vector3.MoveTowards(transform.position, circlePosition, 0.1f);
		await Task.Delay(10);
		}
	}

	public async Task MovePartsToInitial(Vector3 vector)
	{
		if (this.gameObject == null)
		{
			return;
		}
		while (transform.position != initialPosition && this.gameObject !=null)
		{
			transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.1f);
			await Task.Delay(10);
		}
	}

	public async Task MovePartsToCenter(Vector3 vector)
	{
		if (this.gameObject == null)
		{
			return;
		}
		while (transform.position != centerPosition && this.gameObject != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, centerPosition, 0.1f);
			await Task.Delay(10);
		}
	}

	private void SpawnEnemyOnDeath()
	{
		for (int i = 0; i < 8; i++)
		{
			var enemySpawnPosition = new Vector3(UnityEngine.Random.Range(-7, 7), 13);
			var newEnemy = Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
		}	
	}

	public void SetPostion(Vector3 position)
	{
		this.position = position;
	}

	private void InstantiateFractions()
	{
		var fraction = Instantiate(fractionPrefab, transform.position, Quaternion.identity);
		fraction.transform.parent = boss.gameObject.transform;
		fraction.SetDestroyed();
	}
	public void SetDestroyed()
	{
		isDestroyed = true;
	}
}
