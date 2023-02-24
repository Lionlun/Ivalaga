using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern2 : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 enemyLeapForce = new Vector3(20f, 0f, 0f);
    Vector3 enemySmallLeapForce = new Vector3(10f, 0f, 0f);
    Vector3 startDownMovement= new Vector3(0f, -20f, 0f);



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(MovementPattern());
    }

    private IEnumerator MovementPattern()
    {
        rb.AddForce(startDownMovement, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        while (true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            rb.velocity = Vector3.zero;

            rb.AddForce(-enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector3.zero;
            rb.AddForce(enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector3.zero;

            rb.AddForce(-enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            rb.velocity = Vector3.zero;
            rb.AddForce(-enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
            rb.velocity = Vector3.zero;

            rb.AddForce(enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector3.zero;
            rb.AddForce(-enemySmallLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector3.zero;

            rb.AddForce(enemyLeapForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
        }
        
        
    }
}