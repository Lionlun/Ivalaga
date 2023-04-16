using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerThirdPhase", menuName = "ScriptableObjects/PlayerThirdPhase", order = 1)]
public class PlayerThirdPhase :ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public Player player;

    public PlayerGun gun;

    PlayerCompressedState playerCompressedState;

	public bool IsTier3 { get; private set; }

    public void Enter()
    {
        Debug.Log("Enter Third phase");

        IsTier3= true;
        animator.SetBool("IsThird", true);
        animator.SetBool("IsSecond", false);
        animator.SetBool("IsThirdDamaged", false);
    }

    public void Exit()
    {
        Debug.Log("Exit Third phase");
        animator.SetBool("IsThird", false);
        IsTier3 = false;
    }

    public void Update()
    {
        if (player.Points < 800)
        {
           player.SetThirdDamagedPhase();
        }
    }

    public void Shoot<T>(T bulletType) where T : IPlayerBullet
    {
        gun.Shoot(bulletType);
	}

    public void Init(Animator animator, Rigidbody2D rb, Player player, PlayerGun gun)
    {
        this.animator = animator;
        this.player = player;
        this.gun = gun;
    }
}
