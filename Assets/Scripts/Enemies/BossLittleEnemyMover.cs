using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BossLittleEnemyMover:MonoBehaviour
{
	private Vector3 offset;
	static float offsetY = 0;
	Boss boss;
	List<int> ints = new List<int>();
	BossLittleEnemyTier2 tier2;
	BossLittleEnemy littleEnemy;
	private float distanceToBoss = 4;
	public static Dictionary<int, bool> rawList = new Dictionary<int, bool>() //плохо, что просто не могу размер ряда установить
	
	{
		{ -3, false },
		{ -2, false },
		{ -1, false },
		{ 0, false },
		{ 1, false },
		{ 2, false },
		{ 3, false },
		{ 4, false },
	};
	private void OnEnable()
	{
		BossPart.OnDestroyed += ResetOffsetY;
		BossPart.OnDestroyed += ResetRawList;
	}
	private void OnDisable()
	{
		BossPart.OnDestroyed -= ResetOffsetY;
		BossPart.OnDestroyed -= ResetRawList;
	}
	private void Start()
	{
		boss = FindObjectOfType<Boss>();
		littleEnemy = GetComponent<BossLittleEnemy>();
		OccupyPosition();
	}
	private void Update()
	{
		Move();
	}
	public void Move()
	{
		if (boss != null)
		{
			var locationNeeded = boss.transform.position + offset;
			transform.position = Vector3.MoveTowards(transform.position, locationNeeded, 14f * Time.deltaTime);
		}
	}

	private async void OccupyPosition()
	{
		
		var littleEnemyOffsetX = Random.Range(-3, 5); // плохо, что это значение зависит от словаря

		while (rawList[littleEnemyOffsetX])
		{
			if (rawList.Values.All(value => value))
			{
				break;
			}
			littleEnemyOffsetX = Random.Range(-3, 5); // плохо, что это значение зависит от словаря
		}
		var littleEnemyOffsetXY = new Vector3(littleEnemyOffsetX, offsetY - distanceToBoss);

		foreach (var element in rawList)
		{
			if (element.Key == littleEnemyOffsetXY.x)
			{
				ints.Add(element.Key);
			}
		}

		foreach (var element in ints)
		{
			rawList[element] = true;
		}
		
		SetOffset(littleEnemyOffsetXY);
		littleEnemy.SetOffset(littleEnemyOffsetXY);
		if (rawList.Values.All(value => value))
		{
			offsetY += 1;

			ResetRawList();
		}
		await Task.Delay(100);
	}

	public void ResetRawList()
	{
		foreach (var key in rawList.Keys.ToList())
		{
			rawList[key] = false;
		}
	}
	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
	}
	public void ResetOffsetY()
	{
		offsetY = 0;
	}
}
