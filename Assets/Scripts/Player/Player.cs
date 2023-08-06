using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
	public static event Action<int> OnPlayerDeath;

	CharacterController2D characterController;
	PlayerPoints playerPoints;
	Health health;

	PlayerUI playerUI;
	HealthBar healthBar;
	SpriteRenderer spriteRenderer;
	Color32 damageColor = new Color32(255, 124, 124, 245);

	void Awake()
    {
       GlobalEvents.OnEnemyKilled.AddListener(GetHealth);
    }
    void Start()
    {
		playerPoints = GetComponent<PlayerPoints>();
		health = GetComponent<Health>();
		playerUI = GetComponentInChildren<PlayerUI>();
		characterController = GetComponent<CharacterController2D>();
		healthBar = FindObjectOfType<HealthBar>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

	public void TakePoints(int points)
	{
		playerPoints.TakePoints(points);
	}
	public void GetPoints()
	{
		playerPoints.GetPoints();
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

	public void Die()
	{
		OnPlayerDeath?.Invoke(0);
	}
}
