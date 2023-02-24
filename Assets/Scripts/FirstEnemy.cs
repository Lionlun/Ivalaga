using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : EnemyBaseClass
{
    public FirstEnemy()
    {
        enemyHealth = 100f;
    }
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private Transform shootingPoint;

    float TimerForNextAttack, Cooldown;

    void Start()
    {
        Cooldown = 1;
        TimerForNextAttack = Cooldown;
    }

    
    void Update()
    {
        
        EnemyAttack(10);
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    

    protected override void EnemyAttack(float damage)
    {
        if (TimerForNextAttack > 0)
        {
            TimerForNextAttack -= Time.deltaTime;
        }
        else if (TimerForNextAttack <= 0)
        {
            Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);
            TimerForNextAttack = Cooldown;
        }
       
    }

    public override void EnemyTakeDamage(float damage)
    {
        enemyHealth -= damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            Debug.Log("EnemyHit");
            EnemyTakeDamage(10);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

       


    }

}

   

