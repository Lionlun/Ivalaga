using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator animator;

    #region ship's properties

    private int currentHealth;
    [SerializeField] private int maxHealth = 500;
    #endregion

    [SerializeField] private PlayerGun gun;
    public Rigidbody2D rb { get; set; }

    public int Points = 0; //Возможно заменить на евент

    private int pointsToGet = 100;

    public HealthBar healthBar;

    SceneManagerScript sceneManager;

	private void Awake()
    {
        GlobalEvents.OnEnemyKilled.AddListener(EnemyKilled);
        GlobalEvents.OnEnemyKilled.AddListener(GetHealth);
        GlobalEvents.OnHealthPackPickUp.AddListener(GetHealth);
    }
    void Start()
    {
        currentHealth = maxHealth;
		sceneManager = FindObjectOfType<SceneManagerScript>();

		healthBar.SetMaxHealth(currentHealth);
		rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MaxPoints();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

	public void TakePoints(int points)
	{
		Points -= points;
	}

	private void Death()
    {
        currentHealth = 0;
        sceneManager.LoadScene(0);
		Destroy(gameObject);
    }

    public void TakeDamage(int ownBulletDamage)
    {
        currentHealth -= ownBulletDamage;
        healthBar.SetHealth(currentHealth);
    }

    public void GetHealth()
    {
        currentHealth += 50;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
		healthBar.SetHealth(currentHealth);
	}
  
    void EnemyKilled()
    {
        Points += pointsToGet;
    }

    private void MaxPoints()
    {
        if (Points > 1300)
        {
            Points = 1200;
        }
    }
}
