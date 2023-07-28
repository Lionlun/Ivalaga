using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BossWarning : MonoBehaviour
{
    TextMeshProUGUI text;
    byte t = 0;
    
    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		text.color = new Color32(255, 42, 42, t);
	}

	private void Update()
	{
		if (t > 255)
		{
			t = 0;
		}
	}
	void FixedUpdate()
    {
		text.color = new Color32(255, 42, 42, t);
		t += 5;
	}

    
}
