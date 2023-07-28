using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyMovement : MonoBehaviour
{
	[HideInInspector] public Vector3 Direction;

	Vector3 relativePos;
	List<Vector2> path;

	int pointIndex = 0;
	float speed = 7f;

	CurvePathPointPlacer curvePathPlacer;

	void Start()
    {
		curvePathPlacer = FindObjectOfType<CurvePathPointPlacer>();
		path = curvePathPlacer.Waypoints;
	}

    void Update()
    {
		Move();
	}

	private void Move()
	{
		if (pointIndex <= path.Count && curvePathPlacer.IsPlaced)
		{
			if (pointIndex + 4 > path.Count)
			{
				pointIndex = 0;
			}
			transform.position = Vector2.MoveTowards(transform.position, path[pointIndex], speed * Time.deltaTime);

			var currentPosition = new Vector2(transform.position.x, transform.position.y);
			relativePos = path[pointIndex + 1] - currentPosition;
			float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			Direction = path[pointIndex + 3] - path[pointIndex];

			if (transform.position == new Vector3(path[pointIndex].x, path[pointIndex].y))
			{
				pointIndex++;
			}
		}
	}
}
