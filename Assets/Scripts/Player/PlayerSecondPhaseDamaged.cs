using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhaseDamaged", menuName = "ScriptableObjects/PlayerSecondPhaseDamaged", order = 1)]

public class PlayerSecondPhaseDamaged : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public PlayerBehaviour playerBehaviour;
	public Player player;

    public PlayerGun gun;

	private int pointsToNextPhase = 600;
	private int pointsToPreviousPhase = 400;

	public bool IsTier2 { get; private set; }

    public void Enter()
    {
        animator.SetBool("IsSecondDamaged", true);
        animator.SetBool("IsSecond", false);
    }

    public void Exit()
    {
        animator.SetBool("IsSecondDamaged", false);
    }

    public void Update()
    {

        if (player.Points > pointsToNextPhase)
        {
            playerBehaviour.SetSecondPhase();
        }

        if (player.Points < pointsToPreviousPhase)
        {
            playerBehaviour.SetFirstPhase();
        }
    }

	public void Shoot(PlayerBulletBase bulletType)
	{
		gun.Shoot(bulletType);
	}

	public void Init(Animator animator, PlayerBehaviour playerBehaviour, PlayerGun gun , Player player)
    {
        this.animator = animator;
		this.playerBehaviour = playerBehaviour;
		this.player = player;
		this.gun = gun;
    }
}
