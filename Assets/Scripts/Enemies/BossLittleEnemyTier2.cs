using UnityEngine;

public class BossLittleEnemyTier2 : EnemyBaseClass, IHealth
{
	#region Attack
	private float timerForNextAttack;
	private float cooldown = 3f;
	[SerializeField] EnemyBulletBehaviour enemyBullet;
	[SerializeField] Transform shootingPoint;
	#endregion

	public Boss Boss;
	private Vector3 offset;

	private void OnEnable()
	{
		Boss.OnBossDestroyed += Die;
	}

	private void OnDisable()
	{
		Boss.OnBossDestroyed -= Die;
	}

	void Start()
	{
		timerForNextAttack = cooldown;
		Boss = FindObjectOfType<Boss>();
	}
	void Update()
	{
		EnemyAttack();
		Move();
	}

	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
	}

	protected override void EnemyAttack()
	{
		if (timerForNextAttack > 0)
		{
			timerForNextAttack -= Time.deltaTime;
		}
		else if (timerForNextAttack <= 0)
		{
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);

			timerForNextAttack = cooldown;
		}
	}

	void Move()
	{
		if (Boss != null)
		{
			var locationNeeded = new Vector2 (Boss.transform.position.x+offset.x, Boss.transform.position.y+offset.y);
			transform.position = Vector3.MoveTowards(transform.position, locationNeeded, 12f * Time.deltaTime);
		}
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<CharacterController2D>())
		{
			var player = collision.GetComponentInParent<CharacterController2D>();
			var pushBackVector = transform.position - player.transform.position;
			player.transform.position = Vector3.MoveTowards(player.transform.position, pushBackVector * 2, 0.1f);
		}
	}
}
