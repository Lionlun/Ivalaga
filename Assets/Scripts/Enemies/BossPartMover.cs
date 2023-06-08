using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossPartMover : MonoBehaviour
{
    List<BossPart> bossParts;
	Boss boss;
	List<Vector3> circlevectors= new List<Vector3>();
	List<Vector3> initialVectors = new List<Vector3>();
	List<Vector3> centervectors = new List<Vector3>();

	BossPartPositionSetter positionSetter;

	public bool isCircle;
	public bool isInitial;

	void Start()
    {
		positionSetter = new BossPartPositionSetter();
		boss = FindObjectOfType<Boss>();
		bossParts = boss.BossParts;

		StartCoroutine(CycleBoss());
	}

	private void Update()
	{
	if (isCircle)
		{
			MoveToCircleVectors();
			isCircle = false;
		}
		if (isInitial)
		{
			MoveToInitialVectors();
			isInitial= false;
		}
	}

	private void OnEnable()
	{
		Boss.OnBossDestroyed += StopCoroutineOnDestroy;
	}
	private void OnDisable()
	{
		Boss.OnBossDestroyed -= StopCoroutineOnDestroy;
	}

	private void MoveToInitialVectors()
	{
		initialVectors = positionSetter.SetInitialVectors(boss.transform.position);

		for (int i = 0; i < bossParts.Count; i++)
		{
			bossParts[i].SetInitialPosition(initialVectors[i]);
		}
		MoveToInitial();
	}
	private void MoveToCircleVectors()
    {
		circlevectors = positionSetter.SetCircleVectors(boss.transform.position);

		for (int i = 0; i < bossParts.Count; i++)
		{
			bossParts[i].SetCirclePosition(circlevectors[i]);
		}
		MoveToCircle();
	}
	private void MoveToCenterVectors()
	{
		centervectors = positionSetter.SetCenterVectors(boss.transform.position);

		for (int i = 0; i < bossParts.Count; i++)
		{
			bossParts[i].SetCenterPosition(centervectors[i]);
		}
		MoveToCenter();
	}

	private async void MoveToCircle()
	{
		boss.Stop();
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToCircle();
		}
		await Task.WhenAll(tasks);
		boss.StartMove();
	}
	
	private async void MoveToInitial()
	{
		boss.Stop();
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToInitial(initialVectors[i]);
		}
		await Task.WhenAll(tasks);
		boss.StartMove();
	}

	private async void MoveToCenter()
	{
		boss.Stop();
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToCenter(centervectors[i]);
		}
		await Task.WhenAll(tasks);
		boss.StartMove();
	}

	private IEnumerator CycleBoss() //Это должно быть в другом классе
	{
		yield return new WaitForSeconds(5);
		MoveToCircleVectors();
		boss.SetBehaviourCircle();
		
		yield return new WaitForSeconds(19);
		MoveToCenterVectors();
		boss.SetBehaviourCenter();

		yield return new WaitForSeconds(5);
		MoveToInitialVectors();
		boss.SetBehaviourInitial();
		yield return null;
		StartCoroutine(CycleBoss());
	}

	private void StopCoroutineOnDestroy()
	{
		StopAllCoroutines();
	}
}
