using UnityEngine;

internal class Bullet3 : MonoBehaviour, IPlayerBullet
{
    [SerializeField] BulletBehaviour Bullet;
    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;

    private int bulletOwnDamage = 30;
    public void Shoot()
    {
   
        Instantiate(Bullet, ShootingPoint.position, Quaternion.identity);
        Player.OwnDamage(bulletOwnDamage);
    }

}