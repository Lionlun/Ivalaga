using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSecondPhaseDamaged", menuName = "ScriptableObjects/PlayerSecondPhaseDamaged", order = 1)]

public class PlayerSecondPhaseDamaged : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public Player player;
    Rigidbody2D rb;

    public PlayerGun gun;

    public bool IsTier2 { get; private set; }


    public void Enter()
    {
        Debug.Log("Enter Second damaged phase");
        animator.SetBool("IsSecondDamaged", true);
        animator.SetBool("IsSecond", false);
    }

    public void Exit()
    {
        animator.SetBool("IsSecondDamaged", false);
        Debug.Log("Exit Second damaged phase");
    }

    public void Update()
    {
        Debug.Log("Update Second damaged phase");

        if (player.Points > 400)
        {
            player.SetSecondPhase();
        }

        if (player.Points < 200)
        {
            player.SetFirstPhase();
        }

    }

    public void Shoot<T>(T bulletType) where T : IPlayerBullet
    {
		gun.Shoot(bulletType);
	}
    public void Init(Animator animator, Rigidbody2D rb, Player player, PlayerGun gun)
    {
        this.animator = animator;
        this.rb = rb;
        this.player = player;
        this.gun = gun;
    }
}
