using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour, IPlayerBullet
{
    [SerializeField] BulletBehaviour Bullet;
    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;
    private int simpleBulletOwnDamage = 20;

   
    
    public void Shoot()
    {
        
        Debug.Log("I shoot bullet2");

        Instantiate(Bullet, ShootingPoint.position, Quaternion.identity);
        Player.OwnDamage(simpleBulletOwnDamage);
    }

     

    

   
}
