using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet5 : MonoBehaviour, IPlayerBullet
{
    
    [SerializeField] BulletBehaviour BulletPrefab;



    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;




    private int bulletOwnDamage = 50;


    public void Shoot()
    {

        Instantiate(BulletPrefab, ShootingPoint.position + new Vector3(Random.Range(-0.5f, 0.4f), Random.Range(0, 0.5f)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360.0f)));

        Player.OwnDamage(bulletOwnDamage);

    }





}
