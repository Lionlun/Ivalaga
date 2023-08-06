using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerThirdPhase", menuName = "ScriptableObjects/PlayerThirdPhase", order = 1)]
public class PlayerThirdPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
	public Player Player;
    public PlayerPoints PlayerPoints;
	public PlayerGun Gun;

	private int pointsToPreviousPhase = 1000;

    public void Enter()
    {
        Animator.SetBool("IsThird", true);
        Animator.SetBool("IsSecond", false);
        Animator.SetBool("IsThirdDamaged", false);
    }

    public void Exit()
    {
        Animator.SetBool("IsThird", false);
    }

    public void Update()
    {
        if (PlayerPoints.Points < pointsToPreviousPhase)
        {
           PlayerBehaviour.SetThirdDamagedPhase();
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
