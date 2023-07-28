using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerThirdPhaseDamaged", menuName = "ScriptableObjects/PlayerThirdPhaseDamaged", order = 1)]
public class PlayerThirdPhaseDamage : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public PlayerBehaviour playerBehaviour;
	public Player player;
    public PlayerGun gun;

	private int pointsToNextPhase = 1200;
	private int pointsToPreviousPhase = 800;

	public void Enter()
    {
        animator.SetBool("IsThirdDamaged", true);
        animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        animator.SetBool("IsThirdDamaged", false);
    }

    public void Update()
    {
        if (player.Points < pointsToPreviousPhase)
        {
            playerBehaviour.SetSecondPhase();
        }
        if (player.Points> pointsToNextPhase) 
        {
            playerBehaviour.SetThirdPhase();
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
