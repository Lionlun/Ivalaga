using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Dictionary<Type, IPlayerBehaviour> behavioursMap { get; set; }
    public IPlayerBehaviour behaviourCurrent;

    public Animator animator;

    #region ship's properties
    [SerializeField] float shipSpeed = 1;

    [SerializeField] private int shipHealth = 100;
    #endregion


    [SerializeField] private PlayerGun gun;
    public Rigidbody2D rb { get; set; }  //public???

    private bool isTier1 = true;
    public bool IsTier2 { get; set; }
    public bool IsTier3 { get; set; }

    public int Points = 0; //Возможно заменить на евент

    private int pointsToGet = 100;

    private void Awake()
    {
        GlobalEvents.OnEnemyKilled.AddListener(EnemyKilled);
        GlobalEvents.OnEnemyKilled.AddListener(GetHealth);
        GlobalEvents.OnHealthPackPickUp.AddListener(GetHealth);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.InitBehaviours();
        SetBehaviourByDefault();
    }

    void Update()
    {
        this.behaviourCurrent.Update();
        MaxPoints();

        if (shipHealth <= 0)
        {
            Death();
        }
    }

    public void ShipMovement(float xMovement, float yMovement)
    {
        rb.AddForce(new Vector3(xMovement, yMovement, 0) * shipSpeed); 
    }

	public void SetFirstPhase()
	{
		var behaviour = this.GetBehaviour<PlayerFirstPhase>();
		this.SetBehaviour(behaviour);
	}

	public void SetSecondPhase()
	{
		var behaviour = this.GetBehaviour<PlayerSecondPhase>();
		this.SetBehaviour(behaviour);
	}

	public void SetSecondDamagedPhase()
	{
		var behaviour = this.GetBehaviour<PlayerSecondPhaseDamaged>();
		this.SetBehaviour(behaviour);
	}

	public void SetThirdPhase()
	{
		var behaviour = this.GetBehaviour<PlayerThirdPhase>();
		this.SetBehaviour(behaviour);
	}

	public void SetThirdDamagedPhase()
	{
		var behaviour = this.GetBehaviour<PlayerThirdPhaseDamage>();
		this.SetBehaviour(behaviour);
	}

	public Type GetCurrentBehaviour()
	{
		var currentBehaviour = behaviourCurrent.GetType();

		return currentBehaviour;
	}

	public void TakePoints(int points)
	{
		Points -= points;
	}

	private void Death()
    {
        shipHealth = 0;
    }

    public void OwnDamage(int ownBulletDamage)
    {
        shipHealth -= ownBulletDamage;
        Debug.Log(shipHealth);
    }

    public void GetHealth()
    {
        shipHealth += 10;
        if (shipHealth > 100)
        {
            shipHealth = 100;
        }
    }
  
    void EnemyKilled()
    {
        Points += pointsToGet;
    }

    private void InitBehaviours()
    {
        behavioursMap = new Dictionary<Type, IPlayerBehaviour>();

        PlayerFirstPhase playerFirstPhase = ScriptableObject.CreateInstance("PlayerFirstPhase") as PlayerFirstPhase;
        playerFirstPhase.Init(animator, rb, this, gun);

        PlayerSecondPhase playerSecondPhase = ScriptableObject.CreateInstance("PlayerSecondPhase") as PlayerSecondPhase;
        playerSecondPhase.Init(animator, rb, this, gun);

        PlayerThirdPhase playerThirdPhase = ScriptableObject.CreateInstance("PlayerThirdPhase") as PlayerThirdPhase;
        playerThirdPhase.Init(animator, rb, this, gun);

        PlayerSecondPhaseDamaged playerSecondPhaseDamaged = ScriptableObject.CreateInstance("PlayerSecondPhaseDamaged") as PlayerSecondPhaseDamaged;
        playerSecondPhaseDamaged.Init(animator, rb, this, gun);

        PlayerThirdPhaseDamage playerThirdPhaseDamaged = ScriptableObject.CreateInstance("PlayerThirdPhaseDamage") as PlayerThirdPhaseDamage;
        playerThirdPhaseDamaged.Init(animator, rb, this, gun);

        this.behavioursMap[typeof(PlayerFirstPhase)] = playerFirstPhase;
        this.behavioursMap[typeof(PlayerSecondPhase)] = playerSecondPhase;
        this.behavioursMap[typeof(PlayerSecondPhaseDamaged)] = playerSecondPhaseDamaged;
        this.behavioursMap[typeof(PlayerThirdPhase)] = playerThirdPhase;
        this.behavioursMap[typeof(PlayerThirdPhaseDamage)] = playerThirdPhaseDamaged;
    }

    private void SetBehaviour(IPlayerBehaviour newBehaviour)
    {
        if (this.behaviourCurrent != null)
        {
			this.behaviourCurrent.Exit();
		}

        this.behaviourCurrent = newBehaviour;
        this.behaviourCurrent.Enter();
    }

    private IPlayerBehaviour GetBehaviour<T>() where T : IPlayerBehaviour
    {
        var type = typeof(T);
        return (T) this.behavioursMap[type];
    }

    private void SetBehaviourByDefault()
    {
        SetFirstPhase();
    }

    private void MaxPoints()
    {
        if (Points > 1100)
        {
            Points = 1000;
        }
    }
}
