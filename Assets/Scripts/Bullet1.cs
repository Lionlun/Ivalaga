using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet1 : MonoBehaviour, IPlayerBullet
{
    [SerializeField] BulletBehaviour Bullet;
    [SerializeField] BulletBehaviour[] BulletPrefabs;



    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;

    [SerializeField] float Speed;


    private int simpleBulletOwnDamage = 10;
    

   
   

    private void Start()
    {
       
    }
    public Bullet1()
    {
        
    }
    void Update()
    {
        
    }


    public void Shoot()
    {

       
        Debug.Log("I shoot simple bullet");


        Instantiate(BulletPrefabs[Random.Range(0, BulletPrefabs.Length)], ShootingPoint.position + new Vector3(Random.Range(-0.5f, 0.4f), Random.Range(0, 0.5f)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360.0f)));
        
        Player.OwnDamage(simpleBulletOwnDamage);

            
        
       
    }



   
}
