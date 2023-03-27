using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Player player;
    public PlayerGun gun;
    public Animator animator;

    [SerializeField]float timerForNextAttack, cooldown;

    [SerializeField] ScriptableObject playerFirstPhase;
    [SerializeField] ScriptableObject playerSecondPhase;
    [SerializeField] ScriptableObject playerSecondPhaseDamaged;
    [SerializeField] ScriptableObject playerThirdPhase;
    [SerializeField] ScriptableObject playerThirdPhaseDamaged;






    void Start()
    {
        
        cooldown = 0.05f;
        timerForNextAttack = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        ShootTier1();
        ShootTier2();
        ShootTier3();
        
       
       

    }

    private void FixedUpdate()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");
        player.ShipMovement(xMovement, yMovement);
    }

    private void ShootTier1()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            player.behaviourCurrent.Shoot(new Bullet1());
        }
               
    }

    private void ShootTier2()
    {
        if (player.GetCurrentBehaviour() == typeof(PlayerSecondPhase) || player.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                player.behaviourCurrent.Shoot(new Bullet1());
            }

            if (Input.GetKey(KeyCode.X) && player.GetCurrentBehaviour() == typeof(PlayerSecondPhase))
            {
                player.behaviourCurrent.Shoot(new Bullet3());
            }

            if (Input.GetKey(KeyCode.X) && player.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged))
            {
               player.behaviourCurrent.Shoot(new Bullet2());
            }
        }
        
    }

  

    private void ShootTier3()
    {
        if (player.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || player.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                player.behaviourCurrent.Shoot(new Bullet1());
            }

            if (Input.GetKey(KeyCode.X))
            {
                player.behaviourCurrent.Shoot(new Bullet3());
            }

            if (Input.GetKey(KeyCode.C) && player.GetCurrentBehaviour() == typeof(PlayerThirdPhase))
            {
                player.behaviourCurrent.Shoot(new Bullet5());
            }

            if (Input.GetKey(KeyCode.C) && player.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage))
            {
                player.behaviourCurrent.Shoot(new Bullet4());
            }

        }
            
    }

   

    private IPlayerBehaviour GetBehaviour<T>() where T : IPlayerBehaviour
    {
        var type = typeof(T);
        return (T)player.behavioursMap[type];
    }










}
