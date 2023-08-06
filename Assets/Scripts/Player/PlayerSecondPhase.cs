using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhase", menuName = "ScriptableObjects/PlayerSecondPhase", order = 1)]
public class PlayerSecondPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
	public Player Player;
    public PlayerPoints PlayerPoints;
	public PlayerGun Gun;

	private int pointsToNextPhase = 1200;
	private int pointsToPreviousPhase = 600;

    public void Enter()
    {
        Animator.SetBool("IsSecond", true);
        Animator.SetBool("IsFirst", false);
        Animator.SetBool("IsSecondDamaged", false);
        Animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        Animator.SetBool("IsSecond", false);
    }

    public void Update()
    {
        if (PlayerPoints.Points >= pointsToNextPhase)
        {
            PlayerBehaviour.SetThirdPhase();
        }

        if (PlayerPoints.Points < pointsToPreviousPhase)
        {
            PlayerBehaviour.SetSecondDamagedPhase();
        } 
    }

	public void Shoot(PlayerBulletBase bulletType)
	{
		Gun.Shoot(bulletType);
	}

	public void Init(Animator animator, PlayerBehaviour playerBehaviour, PlayerGun gun, Player player, PlayerPoints playerPoints)
    {
        this.Animator = animator;
		this.PlayerBehaviour = playerBehaviour;
		this.Player = player;
		this.Gun = gun;
		this.PlayerPoints = playerPoints;
	}
}
