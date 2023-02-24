using UnityEngine;

internal class Bullet3 : MonoBehaviour, IPlayerBullet
{
    [SerializeField] BulletBehaviour Bullet;
    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;

    private int simpleBulletOwnDamage = 30;
    public void Shoot()
    {
       
        Debug.Log("I shoot bullet3");

        Instantiate(Bullet, ShootingPoint.position, Quaternion.identity);
        Player.OwnDamage(simpleBulletOwnDamage);
    }

}