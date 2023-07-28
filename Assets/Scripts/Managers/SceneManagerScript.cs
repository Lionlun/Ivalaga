
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private Player player;
	private void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex > 0)
		{
			Instantiate(player, Vector3.zero, Quaternion.identity);
		}
	}
	private void OnEnable()
	{
		Player.OnPlayerDeath += LoadScene;
	}
	private void OnDisable()
	{
		Player.OnPlayerDeath -= LoadScene;
	}
	public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
