using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyMovement : EnemyBaseClass
{
    #region spawnValues
    float spawnDownSpeed = 1f;
    #endregion

    Rigidbody2D rb;
   

    float sinCenterY;




    public FirstEnemyMovement()
    {
        
        enemySpeed = 10f;
    }
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
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

   
   
    private IEnumerator EnemyMovementPattern()
    {
        rb.AddForce(-transform.up * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        //while (transform.position.y >= 6.35)
        //{
        //    rb.velocity = new Vector2(0, -enemySpeed);
        //    yield return null;
        //}
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

        //rb.velocity = new Vector2(0, -3f);
        
        //yield return new WaitForSeconds(3);
        //rb.velocity = new Vector2(0, 3f);
        //StartCoroutine(EnemyUpDownOscilation());
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




    protected override void EnemyAttack(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void EnemyTakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
