using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFirstPhase", menuName = "ScriptableObjects/PlayerFirstPhase", order = 1)]
public class PlayerFirstPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public PlayerBehaviour playerBehaviour;
    public Player player;
    public PlayerGun gun;
    private int pointsToNextPhase = 800;
    public bool IsTier1 { get; private set; }

    public void Enter()
    {
        Debug.Log("Enter First phase");
        animator.SetBool("IsFirst", true);
        animator.SetBool("IsSecond", false);
        IsTier1 = true; //нужны ли они вообще?
	}

    public void Exit()
    {
        Debug.Log("Exit First phase");
        animator.SetBool("IsFirst", false);
        IsTier1 = false;
    }

    public void Update()
    {
       if (player.Points > pointsToNextPhase)
       {
            playerBehaviour.SetSecondPhase();
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
