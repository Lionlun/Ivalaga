using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Player Player;
    public PlayerGun Gun;

    [SerializeField]float timerForNextAttack, cooldown;







    void Start()
    {
        cooldown = 0.05f;
        timerForNextAttack = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");
        Player.ShipMovement(xMovement, yMovement);
        
        ShootTier1();
       

        if (Player.IsTier2)
        {
            ShootTier2();
        }

        if (Player.IsTier3 && Player.IsTier2)
        {
            ShootTier3();
        }
       
        

    }
    private void ShootTier1()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (timerForNextAttack > 0)
            {
                timerForNextAttack -= Time.deltaTime;
            }
            else if (timerForNextAttack <= 0)
            {
                Gun.Shoot(new Bullet1());
                timerForNextAttack = cooldown;
            }
                
          

        }
           
    }

    //private IEnumerator CoroutineAttack()
    //{
    //    while (Input.GetKeyDown(KeyCode.Z))
    //    {
            
    //        Gun.Shoot(new Bullet1());
    //        yield return new WaitForSeconds(0.5f);



    //    }
    //}

    private void ShootTier2()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Gun.Shoot(new Bullet2());

        }
    }

    private void ShootTier3()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Gun.Shoot(new Bullet3());
        }
    }

   
}
