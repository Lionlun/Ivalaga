using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyMovement : Movable
{
    #region spawnValues
    float spawnDownSpeed = 1f;
    #endregion

    private float enemySpeed = 10f;

    float sinCenterY;



    void Start()
    {
        StartCoroutine(EnemyMovementPattern());
        StartCoroutine(SineMovement());
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
    }
   
    public override IEnumerator EnemyMovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        while(true)
        {
            while (IsFacingRight())
            {

                rb.velocity = new Vector2(enemySpeed, 0);
                yield return null;

            }
            while (!IsFacingRight())
            {
                rb.velocity = new Vector2(-enemySpeed, 0);
                yield return null;

            }
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
}
