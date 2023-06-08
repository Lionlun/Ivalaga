using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlueEnemy : EnemyBaseClass
{
	[SerializeField] RedEnemyBullet redEnemyBulet;
	[SerializeField] Transform shootingPoint;
	CurvePathPlacer curvePathPlacer;
	
	int pointIndex = 0;
	float speed = 4f;
	float shootCooldown = 0.5f;

	Vector3 direction;
	Vector3 relativePos;
	
	List<Vector2> path;

	void Start()
    {
		EnemyHealth = 125;

		curvePathPlacer = FindObjectOfType<CurvePathPlacer>();

		path = curvePathPlacer.waypoints;
	}

    void Update()
    {
		Move();
		EnemyAttack();
        if (EnemyHealth<=0)
		{
			Die();
		}
    }
	public override void EnemyTakeDamage(float damage)
	{
		EnemyHealth -= damage;
	}

	protected override void EnemyAttack()
	{

		if (direction != null)
		{
			var shootingPointVector = new Vector2(shootingPoint.position.x, shootingPoint.position.y);
			Debug.DrawRay(shootingPointVector, direction * 100, Color.blue);

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

	private void Move()
	{
		if (pointIndex <= path.Count && curvePathPlacer.isPlaced)
		{
			if (pointIndex > path.Count - 2)
			{
				pointIndex = 0;
			}
			transform.position = Vector2.MoveTowards(transform.position, path[pointIndex], speed * Time.deltaTime);

			var currentPosition = new Vector2(transform.position.x, transform.position.y);
			relativePos = path[pointIndex + 1] - currentPosition;
			float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			direction = path[pointIndex + 3] - path[pointIndex]; //выходит за рамки массива на последней точке

			if (transform.position == new Vector3(path[pointIndex].x, path[pointIndex].y))
			{
				pointIndex++;
			}
		}
	}
}
