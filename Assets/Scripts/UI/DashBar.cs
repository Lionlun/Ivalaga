using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider;

	public void SetMaxValue(float dashValue)
	{
		slider.maxValue = dashValue;
		slider.value = dashValue;
	}
	public void DashUpdate(float dashValue)
	{
		slider.value = dashValue;
	}
}
