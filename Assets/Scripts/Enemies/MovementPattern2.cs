using System.Collections;
using UnityEngine;

public class MovementPattern2 : Movable
{
    Vector3 enemyLeapForce = new Vector3(20f, 0f, 0f);
    Vector3 enemySmallLeapForce = new Vector3(10f, 0f, 0f);

    private void Start()
    {
        StartCoroutine(EnemyMovementPattern());
    }

    public override IEnumerator EnemyMovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        while (true)
        {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            Rigidbody.velocity = Vector3.zero;

            Rigidbody.AddForce(-enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            Rigidbody.velocity = Vector3.zero;

            Rigidbody.AddForce(-enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(-enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            Rigidbody.velocity = Vector3.zero;

            Rigidbody.AddForce(enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(-enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            Rigidbody.velocity = Vector3.zero;

            Rigidbody.AddForce(enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
        }
    }
}
