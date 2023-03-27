using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern3 : Movable
{
    Rigidbody2D rb;
    Vector3 diagonalMovementDown = new Vector3(-6f, -6f, 0f);
    Vector3 diagonalMovementUp = new Vector3(-6f, 6f, 0);
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MovementPattern());
        
    }

    private IEnumerator MovementPattern()
    {
        yield return StartCoroutine(base.EnemyMovementPattern());

        
        while (true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(diagonalMovementDown, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector3.zero;
            rb.AddForce(diagonalMovementUp, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector3.zero;
            rb.AddForce(-diagonalMovementDown, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector3.zero;
            rb.AddForce(-diagonalMovementUp, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);

        }
    }
   
}