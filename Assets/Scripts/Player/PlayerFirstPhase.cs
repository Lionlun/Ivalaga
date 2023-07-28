using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFirstPhase", menuName = "ScriptableObjects/PlayerFirstPhase", order = 1)]
public class PlayerFirstPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
    public Player Player;
    public PlayerGun Gun;

    private int pointsToNextPhase = 800;

    public void Enter()
    {
        Animator.SetBool("IsFirst", true);
        Animator.SetBool("IsSecond", false);
	}

    public void Exit()
    {
        Debug.Log("Exit First phase");
        Animator.SetBool("IsFirst", false);
    }

    public void Update()
    {
       if (Player.Points > pointsToNextPhase)
       {
            PlayerBehaviour.SetSecondPhase();
       }
    }

    public void Shoot(PlayerBulletBase bulletType)
    {
        Gun.Shoot(bulletType);
    }

    public void Init(Animator animator, PlayerBehaviour playerBehaviour, PlayerGun gun, Player player)
    {
        this.Animator = animator;
        this.PlayerBehaviour = playerBehaviour;
        this.Player = player;
        this.Gun = gun;
    }
}
