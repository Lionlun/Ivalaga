using UnityEngine;

public class CurvePathCreator : MonoBehaviour
{
	[HideInInspector] public CurvePath CurvePath;

	public void CreatePath()
	{
		CurvePath = new CurvePath();
	}
}
