using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform ShootingPoint;

    public Player Player;
    
    public void Shoot<T>(T playerBulletType) where T : IPlayerBullet
    {
        playerBulletType = GetComponent<T>();

        playerBulletType.Shoot();  
    }
}
