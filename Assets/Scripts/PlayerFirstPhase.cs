using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerFirstPhase", menuName = "ScriptableObjects/PlayerFirstPhase", order = 1)]
public class PlayerFirstPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public Player player;
    public PlayerGun gun;
    Rigidbody2D rb;

    [SerializeField] float timerForNextAttack = 0.01f;
    [SerializeField] float cooldown = 0.01f;

    public bool IsTier1 { get; private set; }

    public void Enter()
    {
        Debug.Log("Enter First phase");
        animator.SetBool("IsFirst", true);
        animator.SetBool("IsSecond", false);
        IsTier1 = true;
    }

    public void Exit()
    {
        Debug.Log("Exit First phase");
        animator.SetBool("IsFirst", false);
        IsTier1 = false;
    }

    public void Update()
    {
       Debug.Log("Update First phase");


       if (player.Points > 600)
       {
            player.SetSecondPhase();
       }
    }

    public void Shoot<T>(T bulletType) where T:IPlayerBullet
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
