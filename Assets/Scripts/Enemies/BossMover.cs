using System.Threading.Tasks;
using UnityEngine;

public class BossMover : MonoBehaviour
{
	public bool IsMovingRight { get; set; } = true;
	public bool IsMovingLeft { get; set; }

	public bool IsPlaced;

	float startSpeed = 0.07f;
	float speedInitial = 0.2f;
	float speedDiamond = 0.34f;

	BossPartMover partMover;
	Boss boss;

	void Start()
	{
		boss = GetComponent<Boss>();
		partMover = GetComponentInChildren<BossPartMover>();
	}
	
	void Update()
    {
		SetDirection();
	}

	void FixedUpdate()
	{
		if (!partMover.IsStopped)
		{
			if (boss.currentBehaviour is BossInitialBehaviour)
			{
				MoveInitPhase();
			}

			if (boss.currentBehaviour is BossCenterBehaviour)
			{
				MoveCenterPhase();
			}
			
			if (boss.currentBehaviour is BossDiamondBehaviour)
			{
				MoveDiamondPhase();
			}
		}
	}
	public async Task FirstMovement()
	{
			while (transform.position.y > 8 && this.gameObject != null)
			{
				transform.Translate(Vector3.down * startSpeed);
				await Task.Delay(20);
			}

			IsPlaced = true;
	}

	void SetDirection()
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

	void MoveInitPhase()
	{
		if (boss.transform.position.y != 8)
		{
			boss.transform.position = new Vector3(boss.transform.position.x, 8);
		}

		if (IsMovingRight)
		{
			boss.transform.position += Vector3.right * speedInitial;
		}
		if (IsMovingLeft)
		{
			boss.transform.position += Vector3.left * speedInitial;
		}
	}

	void MoveDiamondPhase()
	{
		if (IsMovingRight)
		{
			boss.transform.position += Vector3.right * speedDiamond;
		}
		if (IsMovingLeft)
		{
			boss.transform.position += Vector3.left * speedDiamond;
		}
	}
	void MoveCenterPhase()
	{
		boss.transform.position = new Vector3(0, 3);
	}
}
