using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerThirdPhaseDamaged", menuName = "ScriptableObjects/PlayerThirdPhaseDamaged", order = 1)]
public class PlayerThirdPhaseDamage : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public Player player;
    Rigidbody2D rb;

    public PlayerGun gun;

    public bool IsTier3 { get; private set; }


    public void Enter()
    {
        Debug.Log("Enter Third damaged phase");

        animator.SetBool("IsThirdDamaged", true);
        animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        Debug.Log("Exit Third damaged phase");
        animator.SetBool("IsThirdDamaged", false);
    }

    public void Update()
    {
        if (player.Points < 600)
        {
            player.SetSecondPhase();
        }
        if (player.Points> 800) 
        {
            player.SetThirdPhase();
        }
    }

    public void Shoot<T>(T bulletType) where T : IPlayerBullet
    {
		gun.Shoot(bulletType);
	}
    public void Init(Animator animator, Rigidbody2D rb, Player player, PlayerGun gun)
    {
        this.animator = animator;
        this.rb = rb;
        this.player = player;
        this.gun = gun;
    }
}
