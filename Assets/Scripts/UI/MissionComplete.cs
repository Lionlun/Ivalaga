using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MissionComplete : MonoBehaviour
{
	[SerializeField] private SceneManagerScript sceneManagerScript;
	TextMeshProUGUI text;
	float t = 0;

	private void OnEnable()
	{
		Boss.OnBossDestroyed += Appear;
	}
	private void OnDisable()
	{
		Boss.OnBossDestroyed -= Appear;
	}
	private void Start()
	{
		sceneManagerScript = FindObjectOfType<SceneManagerScript>();
		text = GetComponent<TextMeshProUGUI>();
	}

	private async void Appear()
	{
		while (t < 255)
		{
			text.color = new Color32(0, 0, 0, (byte)t);
			t += 1;
			await Task.Delay(10);
		}
		await Task.Delay(1000);
		sceneManagerScript.LoadScene(0); //�� ������ ���������� ��������� �����, �� ���� ���
	}
}
