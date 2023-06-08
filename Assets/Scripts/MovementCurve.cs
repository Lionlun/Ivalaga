using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[ExecuteAlways]
public class MovementCurve : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;
    void Start()
    {
		//Destroy(gameObject, 3);
    }
	private void OnDrawGizmos()
	{
		int segmentsNumber = 20;
		Vector3 previousPoint = p0.position;

		for (int i = 0; i < segmentsNumber; i++)
		{
				float parameter = (float)i / segmentsNumber;
				Vector3 point = BezierCurve.GetPoint(p0.position, p1.position, p2.position, p3.position, parameter);
				Gizmos.DrawLine(previousPoint, point);
				previousPoint = point;
		}
	}
}
