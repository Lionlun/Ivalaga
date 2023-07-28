using System.Collections;
using UnityEngine;

public class YellowEnemyMovement : Movable
{
    float enemySpeed = 0f;
    float sinCenterY;
    bool isMovingRight;
    bool isMovingLeft;
    float oscilationRate = 0.01f;
    void Start()
    {
        StartCoroutine(EnemyMovementPattern());
        StartCoroutine(SineMovement());
    }
	private void Update()
	{
        Acceleration();
	}
	public override IEnumerator EnemyMovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        while(true)
        {

            if (Mathf.Abs(enemySpeed) < 10)
            {
				Acceleration();
			}
			
			Rigidbody.velocity = new Vector2(enemySpeed, 0);

           yield return null;
        }
    }

    private void Acceleration()
    {
        if (Mathf.Abs(enemySpeed) > 10)
        {
            return;
        }
       enemySpeed += Time.deltaTime*2;
    }
    private IEnumerator SineMovement()
    {
        yield return new WaitForSeconds(0.5f);
        sinCenterY = transform.position.y;
        while (true)
        {
            Vector2 pos = transform.position;
            float sin = Mathf.Sin(pos.x) / 2;
            pos.y = Mathf.Lerp(pos.y, sinCenterY + sin, oscilationRate);

            if(oscilationRate < 0.1f)
            {
                oscilationRate += Time.deltaTime/4;
            }
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
