using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerThirdPhase", menuName = "ScriptableObjects/PlayerThirdPhase", order = 1)]
public class PlayerThirdPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public PlayerBehaviour playerBehaviour;
	public Player player;
	public PlayerGun gun;

	private int pointsToPreviousPhase = 1000;

    public void Enter()
    {
        animator.SetBool("IsThird", true);
        animator.SetBool("IsSecond", false);
        animator.SetBool("IsThirdDamaged", false);
    }

    public void Exit()
    {
        animator.SetBool("IsThird", false);
    }

    public void Update()
    {
        if (player.Points < pointsToPreviousPhase)
        {
           playerBehaviour.SetThirdDamagedPhase();
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
