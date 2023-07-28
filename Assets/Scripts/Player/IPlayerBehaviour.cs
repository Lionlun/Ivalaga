
public interface IPlayerBehaviour
{
    void Enter();
    void Exit();
    void Update();

    void  Shoot(PlayerBulletBase bulletType);
}
