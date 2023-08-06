using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public Animator animator;
	public Dictionary<Type, IPlayerBehaviour> behavioursMap { get; set; }
	public IPlayerBehaviour behaviourCurrent;

	private PlayerGun gun;

	Player player;
	PlayerPoints playerPoints;

	void Start()
    {
		player = GetComponent<Player>();
		playerPoints = GetComponent<PlayerPoints>();
		gun = GetComponent<PlayerGun>();
		this.InitBehaviours();
		SetBehaviourByDefault();
	}

    void Update()
    {
		this.behaviourCurrent.Update();
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

	private void InitBehaviours()
	{
		behavioursMap = new Dictionary<Type, IPlayerBehaviour>();

		PlayerFirstPhase playerFirstPhase = ScriptableObject.CreateInstance("PlayerFirstPhase") as PlayerFirstPhase;
		playerFirstPhase.Init(animator, this, gun, player, playerPoints);

		PlayerSecondPhase playerSecondPhase = ScriptableObject.CreateInstance("PlayerSecondPhase") as PlayerSecondPhase;
		playerSecondPhase.Init(animator, this, gun, player, playerPoints);

		PlayerThirdPhase playerThirdPhase = ScriptableObject.CreateInstance("PlayerThirdPhase") as PlayerThirdPhase;
		playerThirdPhase.Init(animator, this, gun, player, playerPoints);

		PlayerSecondPhaseDamaged playerSecondPhaseDamaged = ScriptableObject.CreateInstance("PlayerSecondPhaseDamaged") as PlayerSecondPhaseDamaged;
		playerSecondPhaseDamaged.Init(animator, this, gun, player, playerPoints);

		PlayerThirdPhaseDamage playerThirdPhaseDamaged = ScriptableObject.CreateInstance("PlayerThirdPhaseDamage") as PlayerThirdPhaseDamage;
		playerThirdPhaseDamaged.Init(animator, this, gun, player, playerPoints);

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
		return (T)this.behavioursMap[type];
	}

	private void SetBehaviourByDefault()
	{
		SetFirstPhase();
	}
}
