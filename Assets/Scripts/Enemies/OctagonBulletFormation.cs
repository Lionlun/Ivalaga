using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using System.Linq;

public class OctagonBulletFormation : MonoBehaviour
{
	List<OctagonBullet> octagonBullets = new List<OctagonBullet>();
	static float bulletAngle = 0;
	private void Start()
	{
		foreach(Transform child in transform)
		{
			var bullet = child.GetComponent<OctagonBullet>();
			octagonBullets.Add(bullet);
		}
		

		foreach(var bullet in octagonBullets)
		{
			bullet.angle = bulletAngle;
			bulletAngle += 0.2f;
		}

		Destroy(gameObject, 4f);
	}
	private void Update()
	{
		foreach(var bullet in octagonBullets.ToList())
		{
			if(bullet == null)
			{
				octagonBullets.Remove(bullet);
			}
		}
		NativeArray<Vector3> positionArray = new NativeArray<Vector3>(octagonBullets.Count, Allocator.TempJob);
		NativeArray<float> direction = new NativeArray<float>(octagonBullets.Count, Allocator.TempJob);

		for (int i = 0; i < octagonBullets.Count; i++)
		{
				positionArray[i] = octagonBullets[i].transform.position;
				direction[i] = octagonBullets[i].angle;
		}

		OctagonJob octagonJob = new OctagonJob()
		{
			deltaTime = Time.deltaTime,
			positionArray = positionArray,
			direction = direction,

		};

		JobHandle jobHandle = octagonJob.Schedule(octagonBullets.Count, 100);
		jobHandle.Complete();

		for (int i = 0; i < octagonBullets.Count; i++)
		{
			octagonBullets[i].transform.position = positionArray[i];
		}

		positionArray.Dispose();
		direction.Dispose();
	}
}
