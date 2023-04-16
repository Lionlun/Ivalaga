using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFirstPhase", menuName = "ScriptableObjects/PlayerFirstPhase", order = 1)]
public class PlayerFirstPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator animator;
    public Player player;
    public PlayerGun gun;

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
       Debug.Log("Update First phase");

       if (player.Points > 600)
       {
            player.SetSecondPhase();
       }
    }

    public void Shoot<T>(T bulletType) where T:IPlayerBullet
    {
        gun.Shoot(bulletType);
    }

    public void Init(Animator animator, Rigidbody2D rb, Player player, PlayerGun gun)
    {
        this.animator = animator;
        this.player = player;
        this.gun = gun;
    }
}
