using UnityEngine;


public class Octagon : EnemyBaseClass
{
	[SerializeField] private OctagonBulletFormation bullet;
	[SerializeField] private Transform shootingPoint;

	[SerializeField] private OctagonBulletFormation octagonBulletFormation;

	Animator animator;

	private Vector3 rotationSpeed = new Vector3(0,0,1);

	float attackTiming = 4f;
	float cooldown = 0.1f;
	float timeToDeath = 16f;

	void Start()
    {
		animator = GetComponent<Animator>();
	}

    void Update()
    {
		Rotation();
		SelfDestruction();
		EnemyAttack();
	}

	protected override void EnemyAttack()
	{
		if (attackTiming > 0)
		{
			attackTiming -= Time.deltaTime;
		}
		else if (attackTiming <= 0)
		{
			var newBulletFormation = Instantiate(octagonBulletFormation, shootingPoint.position, Quaternion.Euler(rotationSpeed));
			
			attackTiming = cooldown;
		}
	}

	private void Rotation()
	{
		rotationSpeed += new Vector3(0, 0, 1);
		transform.rotation = Quaternion.Euler(rotationSpeed);
	}

	private void SelfDestruction()
	{
		if (timeToDeath > 0)
		{
			timeToDeath -= Time.deltaTime;
		}

		else
		{
			animator.SetTrigger("IsDead");
		}
	}
}
