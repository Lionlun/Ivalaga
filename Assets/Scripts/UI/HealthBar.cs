using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public Slider Slider;
    public Gradient Gradient;

    public void SetMaxHealth (int health)
    {
        HealthText.text = health.ToString() + " hp";
        Slider.maxValue= health;
        Slider.value= health;
        HealthText.color = Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        HealthText.text = health.ToString() + " hp";
		Slider.value = health;
		HealthText.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
