using System.Collections;
using UnityEngine;

public class MovementPattern3 : Movable
{
    Vector3 diagonalMovementDown = new Vector3(-6f, -6f, 0f);
    Vector3 diagonalMovementUp = new Vector3(-6f, 6f, 0);   

    private void Start()
    {
         StartCoroutine(MovementPattern());
    }

    private IEnumerator MovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        while (true)
        {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(diagonalMovementDown, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(diagonalMovementUp, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(-diagonalMovementDown, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(-diagonalMovementUp, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

        }
    }
}
