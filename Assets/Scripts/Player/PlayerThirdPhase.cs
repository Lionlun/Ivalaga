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

	private int pointsToPreviousPhase = 800;

	public bool IsTier3 { get; private set; }

    public void Enter()
    {

        IsTier3= true;
        animator.SetBool("IsThird", true);
        animator.SetBool("IsSecond", false);
        animator.SetBool("IsThirdDamaged", false);
    }

    public void Exit()
    {
        animator.SetBool("IsThird", false);
        IsTier3 = false;
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
