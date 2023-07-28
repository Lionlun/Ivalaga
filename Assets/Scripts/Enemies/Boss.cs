using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public List<BossPart> BossParts = new List<BossPart>();

	public static event Action OnCreated;

	public static event Action OnBossDestroyed;

	[HideInInspector] public IBossBehaviour currentBehaviour;

	[SerializeField] Transform shootingPoint;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
	[SerializeField] BossRay bossRay;
	[SerializeField] ParticleSystem teleportEffectPrefab;
	[SerializeField] BossPartMover bossPartMover;
	private Dictionary<Type, IBossBehaviour> behavioursMap;

	private BossMover bossMover;

	[SerializeField] ParticleSystem shootEffect;

	private async void Start()
	{
		OnCreated?.Invoke();
		bossMover = GetComponent<BossMover>();
		await bossMover.FirstMovement();
		
		this.InitBehaviours();
		SetBehaviourByDefault();
		StartCoroutine(CycleBoss());
	}
	private void Update()
	{
		if (currentBehaviour!= null)
		{
			currentBehaviour.Update();
		}
	
		if (CheckDeath())
		{
			Die();
		}
	}

	private void Die()
	{
		OnBossDestroyed?.Invoke();
		Destroy(gameObject);
	}
	
	public void SetBehaviourInitial()
	{
		var behaviour = this.GetBehaviour<BossInitialBehaviour>();
		this.SetBehaviour(behaviour);
	}

	public void SetBehaviourCircle()
	{
		var behaviour = this.GetBehaviour<BossDiamondBehaviour>();
		this.SetBehaviour(behaviour);
	}

	public void SetBehaviourCenter()
	{
		var behaviour = this.GetBehaviour<BossCenterBehaviour>();
		this.SetBehaviour(behaviour);
	}

	private void InitBehaviours()
	{
		behavioursMap = new Dictionary<Type, IBossBehaviour>();

		BossInitialBehaviour bossInitialBehaviour = ScriptableObject.CreateInstance<BossInitialBehaviour>();
		bossInitialBehaviour.Init(this, bossMover, enemyBullet, shootingPoint, shootEffect);

		BossDiamondBehaviour bossCircleBehaviour = ScriptableObject.CreateInstance<BossDiamondBehaviour>();
		bossCircleBehaviour.Init(this, bossMover, bossRay, shootingPoint, teleportEffectPrefab);

		BossCenterBehaviour bossCenterBehaviour = ScriptableObject.CreateInstance<BossCenterBehaviour>();
		bossCenterBehaviour.Init(this, enemyBullet, shootingPoint, teleportEffectPrefab);

		this.behavioursMap[typeof(BossInitialBehaviour)] = bossInitialBehaviour;
		this.behavioursMap[typeof(BossDiamondBehaviour)] = bossCircleBehaviour;
		this.behavioursMap[typeof(BossCenterBehaviour)] = bossCenterBehaviour;
	}

	private void SetBehaviour(IBossBehaviour newBehaviour)
	{
		if (this.currentBehaviour != null)
		{
			this.currentBehaviour.Exit();
		}
		this.currentBehaviour = newBehaviour;
		this.currentBehaviour.Enter();
	}

	private IBossBehaviour GetBehaviour<T>() where T : IBossBehaviour
	{
		var type = typeof(T);
		return this.behavioursMap[type];
	}

	private void SetBehaviourByDefault()
	{
		this.SetBehaviourInitial();
	}

	private bool CheckDeath()
	{
		return BossParts.All(x => x.IsDestroyed == true);
	}

	private IEnumerator CycleBoss()
	{
		yield return new WaitForSeconds(5);
		bossPartMover.MoveToCircleVectors();
		SetBehaviourCircle();

		yield return new WaitForSeconds(19);
		bossPartMover.MoveToCenterVectors();
		SetBehaviourCenter();

		yield return new WaitForSeconds(5);
		bossPartMover.MoveToInitialVectors();
		SetBehaviourInitial();
		yield return null;
		StartCoroutine(CycleBoss());
	}
}
