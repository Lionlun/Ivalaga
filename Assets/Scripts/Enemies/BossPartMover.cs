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

	public bool IsCircle;
	public bool IsInitial;
	public bool IsStopped;

	void Start()
    {
		positionSetter = new BossPartPositionSetter();
		boss = FindObjectOfType<Boss>();
		bossParts = boss.BossParts;
	}

	private void Update()
	{
	if (IsCircle)
		{
			MoveToCircleVectors();
			IsCircle = false;
		}
		if (IsInitial)
		{
			MoveToInitialVectors();
			IsInitial= false;
		}
	}

	public void MoveToInitialVectors()
	{
		initialVectors = positionSetter.SetInitialVectors(boss.transform.position);

		for (int i = 0; i < bossParts.Count; i++)
		{
			bossParts[i].SetInitialPosition(initialVectors[i]);
		}
		MoveToInitial();
	}
	public void MoveToCircleVectors()
    {
		circlevectors = positionSetter.SetCircleVectors(boss.transform.position);

		for (int i = 0; i < bossParts.Count; i++)
		{
			bossParts[i].SetCirclePosition(circlevectors[i]);
		}
		MoveToCircle();
	}
	public void MoveToCenterVectors()
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
		IsStopped = true;
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToCircle();
		}
		await Task.WhenAll(tasks);
		IsStopped = false;
	}
	
	private async void MoveToInitial()
	{
		IsStopped = true;
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToInitial(initialVectors[i]);
		}
		await Task.WhenAll(tasks);
		IsStopped = false;
	}

	private async void MoveToCenter()
	{
		IsStopped = true;
		var tasks = new Task[bossParts.Count];

		for (int i = 0; i < bossParts.Count; i++)
		{
			tasks[i] = bossParts[i].MovePartsToCenter(centervectors[i]);
		}
		await Task.WhenAll(tasks);
		IsStopped = false;
	}
}
