using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CurvePathGenerator
{
	public bool IsGenerated;
	float interval;

	CurvePath curvePath;
	[SerializeField, HideInInspector] public List<Vector2> points;

	float rightBorder = 10;
	float leftBorder = -11;
	float bottomBorder = -2;
	float topBorder = 2;

	public void Init(CurvePath curvePath, float interval, List<Vector2> points)
	{
		this.curvePath = curvePath;
		this.interval = interval;
		this.points = points;
	}
	public async Task GenerateCurve()
	{
		var lastPoint = points[curvePath.points.Count - 1] + (Vector2.right) * interval;
		while (lastPoint.x <= rightBorder)
		{
			lastPoint = points[points.Count - 1] + (Vector2.right) * interval;
			curvePath.AddHorizontalSegment(lastPoint, Vector2.right);
		}

		curvePath.AddTurnSegment(lastPoint, Vector2.down);

		while (lastPoint.y >= bottomBorder)
		{
			lastPoint = points[points.Count - 1] + (Vector2.down) * interval;
			curvePath.AddVerticalSegment(lastPoint, Vector2.down);
		}

		curvePath.AddTurnSegment(lastPoint, Vector2.left);

		while (lastPoint.x > leftBorder)
		{
			lastPoint = points[points.Count - 1] + (Vector2.left) * interval;
			curvePath.AddHorizontalSegment(lastPoint, Vector2.left);
		}

		curvePath.AddTurnSegment(lastPoint, Vector2.up);

		while (lastPoint.y < topBorder)
		{
			lastPoint = points[points.Count - 1] + (Vector2.up) * interval;
			curvePath.AddVerticalSegment(lastPoint, Vector2.up);
		}

		curvePath.AddTurnSegment(lastPoint, Vector2.right);

		curvePath.ToggleClosed();
		IsGenerated = true;
		await Task.Delay(200);
	}
}
