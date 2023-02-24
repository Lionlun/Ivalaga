using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    #region ship's properties
    [SerializeField] float shipSpeed = 1;

    [SerializeField] private int shipHealth = 100;
    #endregion

    Rigidbody2D rb;

    private bool isTier1 = true;
    public bool IsTier2 { get; set; }
    public bool IsTier3 { get; set; }

    public int Points = 0; //Возможно заменить на евент

    private int pointsToGet = 100;

    private void Awake()
    {
        GlobalEvents.OnEnemyKilled.AddListener(EnemyKilled);
        GlobalEvents.OnEnemyKilled.AddListener(GetHealth);
        GlobalEvents.OnHealthPackPickUp.AddListener(GetHealth);
    }
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTier();

        if (shipHealth <= 0)
        {
            Death();
        }

        


    }

    public void ShipMovement(float xMovement, float yMovement)
    {

        rb.velocity = new Vector3(xMovement, yMovement, 0) * shipSpeed;

    }


    private void Death()
    {

        //Debug.Log("PlayerDead");
        shipHealth = 0;

    }

    public void OwnDamage(int ownBulletDamage)
    {
        shipHealth -= ownBulletDamage;
        //Debug.Log(shipHealth);
    }

    public void GetHealth()
    {
        shipHealth += 10;
        if (shipHealth > 100)
        {
            shipHealth = 100;
        }

    }

    public void SetTier()
    {
        switch (Points)
        {

            case < 99:
                break;

            case < 200:
                IsTier2 = true;
                IsTier3 = false;
                break;

            case >=200:
                IsTier3 = true;
                break;

        }

        
    }

    void EnemyKilled()
    {
        Points += pointsToGet;
    }
}
