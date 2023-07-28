using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhaseDamaged", menuName = "ScriptableObjects/PlayerSecondPhaseDamaged", order = 1)]

public class PlayerSecondPhaseDamaged : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
	public Player Player;
    public PlayerGun Gun;

	private int pointsToNextPhase = 800;
	private int pointsToPreviousPhase = 400;

	public bool IsTier2 { get; private set; }

    public void Enter()
    {
        Animator.SetBool("IsSecondDamaged", true);
        Animator.SetBool("IsSecond", false);
    }

    public void Exit()
    {
        Animator.SetBool("IsSecondDamaged", false);
    }

    public void Update()
    {

        if (Player.Points > pointsToNextPhase)
        {
            PlayerBehaviour.SetSecondPhase();
        }

        if (Player.Points < pointsToPreviousPhase)
        {
            PlayerBehaviour.SetFirstPhase();
        }
    }

	public void Shoot(PlayerBulletBase bulletType)
	{
		Gun.Shoot(bulletType);
	}

	public void Init(Animator animator, PlayerBehaviour playerBehaviour, PlayerGun gun , Player player)
    {
        this.Animator = animator;
		this.PlayerBehaviour = playerBehaviour;
		this.Player = player;
		this.Gun = gun;
    }
}
