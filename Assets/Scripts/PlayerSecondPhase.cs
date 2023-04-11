using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhase", menuName = "ScriptableObjects/PlayerSecondPhase", order = 1)]
public class PlayerSecondPhase : ScriptableObject, IPlayerBehaviour
{


    public Animator animator;
    public Player player;
    public PlayerGun gun;
    Rigidbody2D rb;

    float timerForNextAttack = 0.1f;
    float cooldown = 0.1f;

    public bool IsTier2 { get; private set; }

 

    public void Enter()
    {
        Debug.Log("Enter Second phase");
        IsTier2 = true;
        animator.SetBool("IsSecond", true);
        animator.SetBool("IsFirst", false);
        animator.SetBool("IsSecondDamaged", false);
        animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        animator.SetBool("IsSecond", false);
        Debug.Log("Exit Second phase");
        IsTier2 = false;
    }

    public void Update()
    {
        Debug.Log("Update Second phase");

        if (player.Points >= 1000)
        {
            player.SetThirdPhase();
        }

        if (player.Points < 400)
        {
            player.SetSecondDamagedPhase();
        }
       
    }

    public void Shoot<T>(T bulletType) where T : IPlayerBullet
    {
        
            if (timerForNextAttack > 0)
            {
                timerForNextAttack -= Time.deltaTime;
            }
            else if (timerForNextAttack <= 0)
            {
                gun.Shoot(bulletType);
                timerForNextAttack = cooldown;
            }


    }
    public void Init(Animator animator, Rigidbody2D rb, Player player, PlayerGun gun)
    {
        this.animator = animator;
        this.rb = rb;
        this.player = player;
        this.gun = gun;
    }
}
