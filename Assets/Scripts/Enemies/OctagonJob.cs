using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public struct OctagonJob : IJobParallelFor
{
	public NativeArray<Vector3> positionArray;
	public NativeArray<float> direction;
	public float deltaTime;

	public void Execute(int index)
	{
		positionArray[index] += new Vector3(Mathf.Cos(direction[index]), Mathf.Sin(direction[index])) * 10 * deltaTime;
	}
}
