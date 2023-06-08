using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirstEnemy : EnemyBaseClass
{
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem particlePrefab;
    Animator animator;

    float TimerForNextAttack, Cooldown;

    void Start()
    {
		EnemyHealth = 100f;
		animator = GetComponent<Animator>();
        Cooldown = 1;
        TimerForNextAttack = Cooldown;
    }
        
    void Update()
    {
        EnemyAttack();
        if (EnemyHealth <= 0)
        {
            Die();
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
            animator.SetTrigger("Recoil");
			Instantiate(enemyBullet, shootingPoint.position, Quaternion.identity);

			TimerForNextAttack = Cooldown;
        }
    }

    public override void EnemyTakeDamage(float damage)
    {
        EnemyHealth -= damage;
    }

	private void OnDestroy()
	{
        var particleEffect = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleEffect.Play();
	}
}

   

