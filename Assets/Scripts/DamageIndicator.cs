using System.Threading.Tasks;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
	Color32 damageColor = new Color32(235, 132, 120, 245);
	SpriteRenderer spriteRenderer;

	void Start()
    {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	public async void ActivateIndicator()
	{
		spriteRenderer.color = damageColor;
		await Task.Delay(50);

		if(spriteRenderer != null)
		{
			spriteRenderer.color = Color.white;
		}
	}
}
