using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region ship's properties
	private int currentHealth;
	[SerializeField] private int maxHealth = 500;
	#endregion

	public Animator animator;

    public int Points = 0; //¬озможно заменить на event

	public static event Action<int> OnPlayerDeath;

	[SerializeField] private PlayerGun gun;

	PlayerUI playerUI;
	Points pointsUI;
	HealthBar healthBar;

	int pointsToGet = 100;

	CharacterController2D characterController;

	SpriteRenderer spriteRenderer;

	Color32 damageColor = new Color32(255, 124, 124, 245);

	void Awake()
    {
        GlobalEvents.OnEnemyKilled.AddListener(EnemyKilled);
       // GlobalEvents.OnEnemyKilled.AddListener(GetHealth); TODO изменить, чтобы сочеталось с компонентом Health
    }
    void Start()
    {
		playerUI = GetComponentInChildren<PlayerUI>();
		characterController = GetComponent<CharacterController2D>();
		pointsUI = FindObjectOfType<Points>();
		healthBar = FindObjectOfType<HealthBar>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(currentHealth);
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
	public async void TakeDamage(int bulletDamage)
	{
		if (!characterController.IsInvincible)
		{
			spriteRenderer.color = damageColor;
			currentHealth -= bulletDamage;
			healthBar.SetHealth(currentHealth);
			await Task.Delay(100);
			spriteRenderer.color = Color.white;
		}
	}

	public void TakeDamageFromOwnBullet(int ownBulletDamage)
	{
		currentHealth -= ownBulletDamage;
		healthBar.SetHealth(currentHealth);
	}

	/*public void GetHealth()
	{
		health.GetHealth(50);
		playerUI.PopUpHealthText();
		healthBar.SetHealth(currentHealth);
	}*/

    void EnemyKilled() // TODO изменить
    {
        GetPoints();
	}

    void MaxPoints()
    {
        if (Points > 1300)
        {
            Points = 1200;
        }
    }

	private void OnDestroy()
	{
		OnPlayerDeath?.Invoke(0);
	}
}
