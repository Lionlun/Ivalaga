using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CurvePath : MonoBehaviour
{
	[SerializeField, HideInInspector] public List<Vector2> points;
	bool isClosed;
	[SerializeField] float interval = 1;
	public bool isGenerated;

	public int NumPoints
	{
		get { return points.Count; }
	}
	public int NumSegments
	{
		get
		{
			return points.Count / 3;
		}
	}
	public Vector2 this[int i]
	{
		get { return points[i]; }
	}

	public async void Start()
	{
		isGenerated= false;
		CreateStartPoints(transform.position);
		await GenerateCurve();
	}

	private async Task GenerateCurve()
	{
		var lastPoint = points[points.Count - 1] + (Vector2.right) * interval;
		while (lastPoint.x <= 10)
		{
			lastPoint = points[points.Count - 1] + (Vector2.right) * interval;
			AddSegmentHorizontal(lastPoint, Vector2.right);
			
		}
		AddTurnSegment(lastPoint, Vector2.down);
		while (lastPoint.y >= -2)
		{
			lastPoint = points[points.Count - 1] + (Vector2.down) * interval;
			AddSegmentVertical(lastPoint, Vector2.down);
		}
	
		AddTurnSegment(lastPoint, Vector2.left);

		while ((lastPoint.x > -11))
		{
			lastPoint = points[points.Count - 1] + (Vector2.left) * interval;
			AddSegmentHorizontal(lastPoint, Vector2.left);
		}
		AddTurnSegment(lastPoint, Vector2.up);
		while (lastPoint.y < 2)
		{
			lastPoint = points[points.Count - 1] + (Vector2.up) * interval;
			AddSegmentVertical(lastPoint, Vector2.up);
			await Task.Delay(200);
		}
		AddTurnSegment(lastPoint, Vector2.right);
		ToggleClosed();
		isGenerated = true;
		await Task.Delay(200);

	} 
	public void CreateStartPoints(Vector2 startPosition)
	{
		points = new List<Vector2>()
		{
			startPosition,
			startPosition + (Vector2.up)*interval,
			startPosition + (Vector2.right + Vector2.up)*interval,
			startPosition + (Vector2.right)*interval,
		};
	}
	public void AddSegmentHorizontal(Vector2 anchorPos, Vector2 direction)
	{
		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x, anchorPos.y-2));
		points.Add(anchorPos);

		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x, anchorPos.y + 2) + (direction * 2));
		points.Add(anchorPos + (direction*2));
	}

	public void AddSegmentVertical(Vector2 anchorPos, Vector2 direction)
	{
		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x-2, anchorPos.y));
		points.Add(anchorPos);

		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x+2, anchorPos.y) + (direction * 2));
		points.Add(anchorPos + (direction * 2));
	}

	public void AddTurnSegment(Vector2 anchorPos, Vector2 direction)
	{
		if (direction == Vector2.down)
		{
			points.Add(points[points.Count - 1] * 2 - points[points.Count - 2] + direction * 2);
			points.Add(new Vector2(anchorPos.x + 1, anchorPos.y - 1));
			points.Add(anchorPos + (direction));
		}
		if (direction == Vector2.left)
		{
			points.Add(points[points.Count - 1] * 2 - points[points.Count - 2] + direction * 2);
			points.Add(new Vector2(anchorPos.x - 1, anchorPos.y + 1));
			points.Add(anchorPos + (direction));
		}
		if (direction == Vector2.up)
		{
			points.Add(points[points.Count - 1] * 2 - points[points.Count - 2] - direction * 2);
			points.Add(new Vector2(anchorPos.x+1, anchorPos.y + 1));
			points.Add(anchorPos + (direction));
		}

		if (direction == Vector2.right)
		{
			points.Add(points[points.Count - 1] * 2 - points[points.Count - 2] - direction * 2);
			points.Add(new Vector2(anchorPos.x-1, anchorPos.y +1));
			points.Add(anchorPos + (direction));
		}
	}
	public Vector2[] GetPointsInSegments(int i)
	{
		return new Vector2[]
		{
			points[i*3], points[i*3+1], points[i*3+2], points[LoopIndex(i*3+3)]
		};
	}

	int LoopIndex(int i)
	{
		return (i + points.Count) % points.Count;
	}


	public Vector2[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
	{
		List<Vector2> evenlySpacedPoints = new List<Vector2>();
		evenlySpacedPoints.Add(points[0]);
		Vector2 previousPoint = points[0];
		float dstSinceLastEvenPoint = 0;

		for (int segmentIndex = 0; segmentIndex < NumSegments; segmentIndex++)
		{
			Vector2[] p = GetPointsInSegments(segmentIndex);
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

	public void ToggleClosed()
	{
		isClosed = !isClosed;
		if (isClosed)
		{
			points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
			points.Add(points[0] * 2 - points[1]);
		}
		else
		{
			points.RemoveRange(points.Count - 2, 2);
		}
	}
}
