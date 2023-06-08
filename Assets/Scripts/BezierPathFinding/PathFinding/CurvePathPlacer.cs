using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePathPlacer : MonoBehaviour
{
	public float spacing = .1f;
	public float resolution = 1f;
	public List<Vector2> waypoints;
	public bool isPlaced;
	CurvePath curvePath;
	void Start()
	{
		curvePath = FindObjectOfType<CurvePath>();
	}

	void Update()
	{
		if (curvePath.isGenerated && !isPlaced)
		{
			Vector2[] points = FindObjectOfType<CurvePath>().CalculateEvenlySpacedPoints(spacing, resolution);
			foreach (Vector2 p in points)
			{
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				go.transform.position = p;
				go.transform.localScale = Vector3.one * spacing * .5f;
				waypoints.Add(go.transform.position);
				Destroy(go);
			}
			isPlaced = true;
		}
	}
}
