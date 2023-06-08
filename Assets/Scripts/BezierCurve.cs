using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve
{
 public static Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		Vector2 p01 = Vector3.Lerp(p0, p1, t);
		Vector2 p12 = Vector3.Lerp(p1, p2, t);
		Vector2 p23 = Vector3.Lerp(p2, p3, t);

		Vector2 p012 = Vector3.Lerp(p01, p12, t);
		Vector2 p123 = Vector3.Lerp(p12, p23, t);
		
		Vector2 p0123 = Vector3.Lerp(p012, p123, t);

		return p0123;
	}
}