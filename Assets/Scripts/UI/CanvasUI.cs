using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI bossWarning;
	void OnEnable()
	{
		Boss.OnCreated += CreateBossWarning;
	}
	void OnDisable()
	{
		Boss.OnCreated -= CreateBossWarning;
	}

	void CreateBossWarning()
	{
		var newBossWarning = Instantiate (bossWarning, Vector3.zero, Quaternion.identity);
		newBossWarning.transform.SetParent(transform, false);
		Destroy(newBossWarning.gameObject, 3);
	}
}
