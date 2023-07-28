using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class BossPart : MonoBehaviour, IHealth
{
	[SerializeField] BossPart fractionPrefab;
	[SerializeField] BossLittleEnemy enemy;

	[SerializeField] public int BossPartNumber;
	Boss boss;

	private Vector3 circlePosition;
	private Vector3 initialPosition;
	private Vector3 centerPosition;

	public static event Action OnDestroyed;
	public bool IsDestroyed;

	void Start()
	{
		boss = FindObjectOfType<Boss>();

		boss.BossParts.Add(this);
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
	
	public void SetDestroyed()
	{
		IsDestroyed = true;
	}

	public void Die()
	{
		boss.BossParts.Remove(this);
		GlobalEvents.SendEnemyKilled();
		InstantiateFractions();
		SpawnEnemyOnDeath();
		OnDestroyed?.Invoke();
		Destroy(gameObject);
	}

	private void InstantiateFractions()
	{
		var fraction = Instantiate(fractionPrefab, transform.position, Quaternion.identity);
		fraction.transform.parent = boss.gameObject.transform;
		fraction.SetDestroyed();
	}

	private void SpawnEnemyOnDeath()
	{
		for (int i = 0; i < 8; i++)
		{
			var enemySpawnPosition = new Vector3(UnityEngine.Random.Range(-7, 7), 13);
			var newEnemy = Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
		}
	}
}
