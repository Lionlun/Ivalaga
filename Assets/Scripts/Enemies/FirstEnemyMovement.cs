using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyMovement : Movable
{
    private float enemySpeed = 10f;

    float sinCenterY;

    void Start()
    {
        StartCoroutine(EnemyMovementPattern());
        StartCoroutine(SineMovement());
    }

	public override IEnumerator EnemyMovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        while(true)
        {
           rb.velocity = new Vector2(enemySpeed, 0);

           yield return null;
        }
    }

    private IEnumerator SineMovement()
    {
        yield return new WaitForSeconds(2.0f);
        sinCenterY = transform.position.y;
        while (true)
        {
            Vector2 pos = transform.position;
            float sin = Mathf.Sin(pos.x) / 2;
            pos.y = sinCenterY + sin;
            transform.position = pos;
            yield return null;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Boundary"))
		{
			enemySpeed = -enemySpeed;
		}
	}
}
