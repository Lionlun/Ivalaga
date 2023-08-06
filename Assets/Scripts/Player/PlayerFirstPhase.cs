using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFirstPhase", menuName = "ScriptableObjects/PlayerFirstPhase", order = 1)]
public class PlayerFirstPhase : ScriptableObject, IPlayerBehaviour
{
    public Animator Animator;
    public PlayerBehaviour PlayerBehaviour;
    public Player Player;
    public PlayerGun Gun;
    public PlayerPoints PlayerPoints;

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
       if (PlayerPoints.Points > pointsToNextPhase)
       {
            PlayerBehaviour.SetSecondPhase();
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
