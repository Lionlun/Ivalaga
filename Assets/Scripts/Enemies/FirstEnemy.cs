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
        EnemyAttack();
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    protected override void EnemyAttack()
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
       
    }
}

   

