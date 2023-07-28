using System.Collections.Generic;
using UnityEngine;

public class CurvePathPointPlacer : MonoBehaviour
{
	public float Spacing = .1f;
	public float Resolution = 1f;
	public List<Vector2> Waypoints;
	public bool IsPlaced;
	CurvePath curvePath;

	void Start()
	{
		curvePath = FindObjectOfType<CurvePath>();
	}

	void Update()
	{
		if (curvePath.IsGenerated && !IsPlaced)
		{
			Vector2[] points = CalculateEvenlySpacedPoints(Spacing, Resolution);
			foreach (Vector2 p in points)
			{
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				go.transform.position = p;
				go.transform.localScale = Vector3.one * Spacing * .5f;
				Waypoints.Add(go.transform.position);
				Destroy(go);
			}
			IsPlaced = true;
		}
	}

	public Vector2[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
	{
		List<Vector2> evenlySpacedPoints = new List<Vector2>();
		evenlySpacedPoints.Add(curvePath.points[0]);
		Vector2 previousPoint = curvePath.points[0];
		float dstSinceLastEvenPoint = 0;

		for (int segmentIndex = 0; segmentIndex < curvePath.NumSegments; segmentIndex++)
		{
			Vector2[] p = curvePath.GetPointsInSegments(segmentIndex);
			float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
			float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + controlNetLength / 2f;
			int divisions = Mathf.CeilToInt(estimatedCurveLength * resolution * 10);
			float t = 0;
			while (t <= 1)
			{
				t += 1f / divisions;
				Vector2 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
				dstSinceLastEvenPoint += Vector2.Distance(previousPoint, pointOnCurve);

				while (dstSinceLastEvenPoint >= spacing)
				{
					float overshootDst = dstSinceLastEvenPoint - spacing;
					Vector2 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overshootDst;
					evenlySpacedPoints.Add(newEvenlySpacedPoint);
					dstSinceLastEvenPoint = overshootDst;
					previousPoint = newEvenlySpacedPoint;
				}
				previousPoint = pointOnCurve;
			}
		}
		return evenlySpacedPoints.ToArray();
	}
}
