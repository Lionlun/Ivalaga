using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider Slider;

	public void SetMaxValue(float dashValue)
	{
		Slider.maxValue = dashValue;
		Slider.value = dashValue;
	}
	public void DashUpdate(float dashValue)
	{
		Slider.value = dashValue;
	}
}
