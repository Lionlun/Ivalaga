using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhase", menuName = "ScriptableObjects/PlayerSecondPhase", order = 1)]
public class PlayerSecondPhase : ScriptableObject, IPlayerBehaviour
{


    public Animator animator;
    public PlayerBehaviour playerBehaviour;
	public Player player;
	public PlayerGun gun;
	private int pointsToNextPhase = 1200;
	private int pointsToPreviousPhase = 600;

	public bool IsTier2 { get; private set; }

    public void Enter()
    {
        IsTier2 = true;
        animator.SetBool("IsSecond", true);
        animator.SetBool("IsFirst", false);
        animator.SetBool("IsSecondDamaged", false);
        animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        animator.SetBool("IsSecond", false);
        IsTier2 = false;
    }

    public void Update()
    {
        if (player.Points >= pointsToNextPhase)
        {
            playerBehaviour.SetThirdPhase();
        }

        if (player.Points < pointsToPreviousPhase)
        {
            playerBehaviour.SetSecondDamagedPhase();
        } 
    }

	public void Shoot(PlayerBulletBase bulletType)
	{
		gun.Shoot(bulletType);
	}

	public void Init(Animator animator, PlayerBehaviour playerBehaviour, PlayerGun gun, Player player)
    {
        this.animator = animator;
		this.playerBehaviour = playerBehaviour;
		this.player = player;
		this.gun = gun;
    }
}
