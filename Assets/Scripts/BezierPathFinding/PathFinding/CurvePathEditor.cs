using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurvePath))]
public class CurvePathEditor : Editor
{
	public CurvePath curvePath;

	private void Awake()
	{
		curvePath = FindObjectOfType<CurvePath>();
	}
	private void OnSceneGUI()
	{
		Draw();
	}
	void Draw()
	{
		for(int i = 0; i<curvePath.NumPoints; i++)
		{
			Handles.color = Color.red;
			Handles.FreeMoveHandle(curvePath[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
		}

		for (int i = 0; i < curvePath.NumSegments; i++)
		{
			Vector2[] points = curvePath.GetPointsInSegments(i);
			Handles.color = Color.black;
			Handles.DrawLine(points[1], points[0]);
			Handles.DrawLine(points[2], points[3]);
			Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
		}
	}
}