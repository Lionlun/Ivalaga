using UnityEngine;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] GameObject healthText;

	public void PopUpHealthText()
	{
		var text = Instantiate (healthText, transform.position, Quaternion.identity);
		text.transform.SetParent(transform, true);        
		Destroy(text, 0.5f);
	}
}
