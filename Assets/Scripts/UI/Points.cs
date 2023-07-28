using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
	public TextMeshProUGUI PointsNumber;

	private void Start()
	{
		SetStartPoints();
	}
	public void SetPoints(int points)
	{
		PointsNumber.text = $"Points: {points}";
		if (points >= 1200)
		{
			PointsNumber.text = $"Max Points";
		}
	}

	void SetStartPoints() 
	{
		PointsNumber.text = $"Points: 0";
	}
}
