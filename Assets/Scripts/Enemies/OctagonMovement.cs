using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonMovement : Movable
{
	private void Start()
	{
		StartCoroutine(EnemyMovementPattern());
	}
	public override IEnumerator EnemyMovementPattern()
	{
		yield return StartCoroutine(base.EnemyMovementPattern());
		
		if (transform.position.x > 5)
		{
			rb.AddForce(new Vector2(-16f, 0f), ForceMode2D.Impulse);
		}
		if (transform.position.x < -5) 
		{
			rb.AddForce(new Vector2(16f, 0f), ForceMode2D.Impulse);
		}
	}
}
