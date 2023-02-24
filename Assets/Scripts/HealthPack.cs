using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPack : MonoBehaviour
{
    //public UnityEvent OnCollision;
    float directionX = 0;
    float sideSpeed = 4f;

    private void Start()
    {
        detrmineDirectionX();
    }
    private void Update()
    {
        ConstantMoveToSide();
    }





    void ConstantMoveToSide()
    {
        
            transform.Translate(directionX*Time.deltaTime*sideSpeed, 0, 0);
           
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            GlobalEvents.SendHealthPackPickedUp();
            Destroy(gameObject);
        }
      
    }
    

    void detrmineDirectionX()
    {
        if (transform.position.x <= 0)
        {
            directionX = 1;


        }
        else
        {
            directionX = -1;
        }

    }
}
