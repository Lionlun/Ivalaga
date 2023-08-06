using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
	public int Points = 0; //Возможно заменить на event
	Points pointsUI;
	int pointsToGet = 100;
	CharacterController2D characterController;


	private void Awake()
	{
		GlobalEvents.OnEnemyKilled.AddListener(GetPoints);
	}
	void Start()
    {
		pointsUI = FindObjectOfType<Points>();
		characterController = GetComponent<CharacterController2D>();
	}

    void Update()
    {
		MaxPoints();
	}

	public void TakePoints(int points)
	{
		if (!characterController.IsInvincible)
		{
			Points -= points;
			pointsUI.SetPoints(Points);

			if (Points < 0)
			{
				Points = 0;
			}
		}
	}
	public void GetPoints()
	{
		Points += pointsToGet;
		pointsUI.SetPoints(Points);

		if (Points > 1200)
		{
			Points = 1200;
		}
	}

	void MaxPoints()
	{
		if (Points > 1300)
		{
			Points = 1200;
		}
	}
}
