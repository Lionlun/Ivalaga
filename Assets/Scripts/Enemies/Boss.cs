using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : EnemyBaseClass
{
	public bool IsMovingRight { get; set; } = true;
	public bool IsMovingLeft { get; set; }

	public List<BossPart> BossParts = new List<BossPart>();

	public static event Action OnCreated;

	public static event Action OnBossDestroyed;

	private bool isStopped;
	private float startSpeed = 0.1f;
    
	[SerializeField] Transform shootingPoint;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
	[SerializeField] BossRay bossRay;
	[SerializeField] ParticleSystem teleportEffectPrefab;

	private Dictionary<Type, IBossBehaviour> behavioursMap;
	private IBossBehaviour currentBehaviour;

	private async void Start()
	{
		await FirstMovement();
		OnCreated?.Invoke();
		this.InitBehaviours();
		SetBehaviourByDefault();
	}
	private void Update()
	{
		EnemyAttack();

		
		if (CheckDeath())
		{
			Die();
		}
		SetDirection();

		if (this.currentBehaviour != null)
		{
			this.currentBehaviour.Update();
		}
	}

	public override void Die()
	{
		OnBossDestroyed?.Invoke();
		base.Die();
	}
	private void InitBehaviours()
	{
		behavioursMap = new Dictionary<Type, IBossBehaviour>();

		BossInitialBehaviour bossInitialBehaviour = ScriptableObject.CreateInstance<BossInitialBehaviour>();
		bossInitialBehaviour.Init(this, enemyBullet, shootingPoint);

		BossDiamondBehaviour bossCircleBehaviour= ScriptableObject.CreateInstance<BossDiamondBehaviour>();
		bossCircleBehaviour.Init(this, bossRay, shootingPoint, teleportEffectPrefab);

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

	private void FixedUpdate()
	{
		if(!isStopped)
		{
			Move();
		}
	}

	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
	}
	public void Stop()
	{
		isStopped = true;
	}
	public void StartMove()
	{
		isStopped = false;
	}
	protected override void EnemyAttack()
	{
		if (currentBehaviour!= null)
		{
			currentBehaviour.Attack();
		}
	}
	private void SetDirection()
	{
		if (transform.position.x < -15)
		{
			IsMovingRight = true;
			IsMovingLeft = false;
		}
		if (transform.position.x > 13)
		{
			IsMovingRight = false;
			IsMovingLeft = true;
		}
	}

	private void Move()
	{
		if (currentBehaviour != null)
		{
			currentBehaviour.Move();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Projectile"))
		{
			Debug.Log("Boss Hit");
		}
	}

	private bool CheckDeath()
	{
		return BossParts.All(x => x.isDestroyed == true);
	}

	private async Task FirstMovement()
	{
		while (transform.position.y > 8)
		{
			transform.Translate(Vector3.down*startSpeed);
			await Task.Delay(20);
		}
	}
}
