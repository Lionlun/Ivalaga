using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteAlways]
public class CurvePath : MonoBehaviour
{
	public bool IsGenerated;
	bool isClosed;
	[SerializeField] float interval = 1;
	[SerializeField, HideInInspector] public List<Vector2> points;

	public int NumPoints
	{
		get { return points.Count; }
	}
	public int NumSegments
	{
		get { return points.Count / 3;}
	}
	public Vector2 this[int i]
	{
		get { return points[i]; }
	}

	public async void Start()
	{
		IsGenerated= false;
		CreateStartPoints(transform.position);
		CurvePathGenerator curvePathGenerator = new CurvePathGenerator();
		curvePathGenerator.Init(this, interval, points);
		await curvePathGenerator.GenerateCurve();
		IsGenerated = true;
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
	public void AddHorizontalSegment(Vector2 anchorPos, Vector2 direction)
	{
		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x, anchorPos.y-2));
		points.Add(anchorPos);

		points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
		points.Add(new Vector2(anchorPos.x, anchorPos.y + 2) + (direction * 2));
		points.Add(anchorPos + (direction*2));
	}

	public void AddVerticalSegment(Vector2 anchorPos, Vector2 direction)
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

	int LoopIndex(int i)
	{
		return (i + points.Count) % points.Count;
	}
}
