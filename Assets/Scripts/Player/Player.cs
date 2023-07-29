using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
	public Animator animator;

    public int Points = 0; //Возможно заменить на event

	public static event Action<int> OnPlayerDeath;

	[SerializeField] private PlayerGun gun;

	PlayerUI playerUI;
	Points pointsUI;
	HealthBar healthBar;

	int pointsToGet = 100;

	CharacterController2D characterController;

	SpriteRenderer spriteRenderer;

	Color32 damageColor = new Color32(255, 124, 124, 245);

	Health health;

	void Awake()
    {
       GlobalEvents.OnEnemyKilled.AddListener(GetPoints);
       GlobalEvents.OnEnemyKilled.AddListener(GetHealth);
    }
    void Start()
    {
		health = GetComponent<Health>();
		playerUI = GetComponentInChildren<PlayerUI>();
		characterController = GetComponent<CharacterController2D>();
		pointsUI = FindObjectOfType<Points>();
		healthBar = FindObjectOfType<HealthBar>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
			health.TakeDamage(bulletDamage);
			healthBar.SetHealth(health.CurrentHealth);
			await Task.Delay(100);
			spriteRenderer.color = Color.white;
		}
	}

	public void TakeDamageFromOwnBullet(int ownBulletDamage)
	{
		health.TakeDamage(ownBulletDamage);
		healthBar.SetHealth(health.CurrentHealth);
	}

	public void GetHealth()
	{
		health.GetHealth(50);
		playerUI.PopUpHealthText();
		healthBar.SetHealth(health.CurrentHealth);
	}

    void MaxPoints()
    {
        if (Points > 1300)
        {
            Points = 1200;
        }
    }

	public void Die()
	{
		OnPlayerDeath?.Invoke(0);
	}
}
