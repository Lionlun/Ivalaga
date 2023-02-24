using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    
    [SerializeField] Transform ShootingPoint;


    //IPlayerBullet playerBulletType;
   

    public Player Player;
    
    
    
    void Start()
    {
        //SetBulletType(new Bullet1());
        
    }


    void Update()
    {
        
    }
    
    //public void SetBulletType(IPlayerBullet bulletType)
    //{
    //    bulletType = GetComponent<IPlayerBullet>();
    //    playerBulletType = bulletType;
    //}

   

    public void Shoot<T>(T playerBulletType) where T : IPlayerBullet
    {
        playerBulletType = GetComponent<T>();


        playerBulletType.Shoot();
  
    }

   
   

}
