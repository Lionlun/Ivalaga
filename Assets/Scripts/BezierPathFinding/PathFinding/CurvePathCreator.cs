using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePathCreator : MonoBehaviour
{
	[HideInInspector] public CurvePath curvePath;

	public void CreatePath()
	{
		curvePath = new CurvePath();
	}
}