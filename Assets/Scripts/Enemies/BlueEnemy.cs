using UnityEngine;

public class BlueEnemy : EnemyBaseClass, IHealth
{
	[SerializeField] RedEnemyBullet redEnemyBulet;
	[SerializeField] Transform shootingPoint;
	Vector3 direction;
	BlueEnemyMovement blueEnemyMovement;

	float shootCooldown = 0.5f;

	void Start()
    {
		blueEnemyMovement = GetComponent<BlueEnemyMovement>();
	}

    void Update()
    {
		direction = blueEnemyMovement.Direction;
		EnemyAttack();
    }

	protected override void EnemyAttack()
	{
		if (direction != null)
		{
			var shootingPointVector = new Vector2(shootingPoint.position.x, shootingPoint.position.y);
			Debug.DrawRay(shootingPointVector, blueEnemyMovement.Direction * 100, Color.blue);

			RaycastHit2D hit = Physics2D.Raycast(shootingPointVector, direction * 100);
			if (hit.collider != null)
			{
				if (shootCooldown > 0)
				{
					shootCooldown -= Time.deltaTime;
				}
				if (hit.collider.gameObject.CompareTag("Player") && shootCooldown <= 0)
				{
					Instantiate(redEnemyBulet, shootingPoint.position, Quaternion.identity);
					shootCooldown = 0.5f;
				}
			}
		}
	}
}
