using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

[BurstCompile]
public struct OctagonJob : IJobParallelFor
{
	public NativeArray<Vector3> PositionArray;
	public NativeArray<float> Direction;
	public float DeltaTime;

	public void Execute(int index)
	{
		PositionArray[index] += new Vector3(Mathf.Cos(Direction[index]), Mathf.Sin(Direction[index])) * 10 * DeltaTime;
	}
}
