using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartPositionSetter
{
	List<Vector3> Circlevectors = new List<Vector3>();
	List<Vector3> initialVectors = new List<Vector3>();
	List<BossPart> bossParts;
	Boss boss;

	void Start()
	{ 
		bossParts = boss.BossParts;
	}
	public List<Vector3> SetCircleVectors(Vector3 startPoint)
	{
		Circlevectors.Clear();
		Vector3 vector = startPoint;
		Circlevectors.Add(vector);

		vector += new Vector3(1, -1);
		Circlevectors.Add(vector);

		vector += new Vector3(1, -1);
		Circlevectors.Add(vector);

		vector += new Vector3(-1, -1);
		Circlevectors.Add(vector);

		vector += new Vector3(-1, -1);
		Circlevectors.Add(vector);

		vector += new Vector3(-1, 1);
		Circlevectors.Add(vector);

		vector += new Vector3(-1, 1);
		Circlevectors.Add(vector);

		vector += new Vector3(1, 1);
		Circlevectors.Add(vector);

		return Circlevectors;
	}

	public List<Vector3> SetInitialVectors(Vector3 startPoint)
	{
		initialVectors.Clear();
		Vector3 vector = startPoint;
		initialVectors.Add(vector);

		vector += new Vector3(1, 0);
		initialVectors.Add(vector);

		vector += new Vector3(-0.5f, -1);
		initialVectors.Add(vector);

		vector += new Vector3(0, -1);
		initialVectors.Add(vector);

		vector += new Vector3(-1, 0.5f);
		initialVectors.Add(vector);

		vector += new Vector3(-1, 0.5f);
		initialVectors.Add(vector);

		vector += new Vector3(4, 0);
		initialVectors.Add(vector);

		vector += new Vector3(-1, -0.5f);
		initialVectors.Add(vector);

		return initialVectors;
	}

	public List<Vector3> SetCenterVectors(Vector3 startPoint)
	{
		initialVectors.Clear();
		Vector3 vector = startPoint;
		initialVectors.Add(vector);

		vector += new Vector3(1, -0.5f);
		initialVectors.Add(vector);

		vector += new Vector3(0.5f, -1f);
		initialVectors.Add(vector);

		vector += new Vector3(-0.5f, -1);
		initialVectors.Add(vector);

		vector += new Vector3(-1, -0.5f);
		initialVectors.Add(vector);

		vector += new Vector3(-1, 0.5f);
		initialVectors.Add(vector);

		vector += new Vector3(-0.5f, 1);
		initialVectors.Add(vector);

		vector += new Vector3(0.5f, 1);
		initialVectors.Add(vector);

		return initialVectors;
	}
}
