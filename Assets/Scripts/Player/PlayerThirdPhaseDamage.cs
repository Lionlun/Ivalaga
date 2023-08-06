using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerThirdPhaseDamaged", menuName = "ScriptableObjects/PlayerThirdPhaseDamaged", order = 1)]
public class PlayerThirdPhaseDamage : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
	public Player Player;
    public PlayerPoints PlayerPoints;
    public PlayerGun Gun;

	private int pointsToNextPhase = 1200;
	private int pointsToPreviousPhase = 800;

	public void Enter()
    {
        Animator.SetBool("IsThirdDamaged", true);
        Animator.SetBool("IsThird", false);
    }

    public void Exit()
    {
        Animator.SetBool("IsThirdDamaged", false);
    }

    public void Update()
    {
        if (PlayerPoints.Points < pointsToPreviousPhase)
        {
            PlayerBehaviour.SetSecondPhase();
        }
        if (PlayerPoints.Points> pointsToNextPhase) 
        {
            PlayerBehaviour.SetThirdPhase();
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
