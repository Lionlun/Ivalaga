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

    [SerializeField] ScriptableObject playerFirstPhase;
    [SerializeField] ScriptableObject playerSecondPhase;
    [SerializeField] ScriptableObject playerSecondPhaseDamaged;
    [SerializeField] ScriptableObject playerThirdPhase;
    [SerializeField] ScriptableObject playerThirdPhaseDamaged;

    [SerializeField] PlayerCompressedState playerCompressed;

	private bool isCompressed;
    private bool zIsPressed;
    private bool xIsPressed;
    private bool cIsPressed;

	void Update()
    {
		InputCheck();
		ShootTier1();
        ShootTier2();
        ShootTier3();
        Shrink();
      
  

	}

    private void FixedUpdate()
    {
		CheatPoints();
		float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");
        player.ShipMovement(xMovement, yMovement);
    }

    private void ShootTier1()
	{
		if (player.GetCurrentBehaviour() == typeof(PlayerFirstPhase))
        {
			if (Input.GetKey(KeyCode.Z))
			{
				player.behaviourCurrent.Shoot(new Bullet1());
			}
		}
    }

    private void ShootTier2()
    {
        if ((player.GetCurrentBehaviour() == typeof(PlayerSecondPhase) || player.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged)) && !isCompressed)
        {
            if (zIsPressed)
            {
				player.behaviourCurrent.Shoot(new Bullet1());
            }

            if (xIsPressed && player.GetCurrentBehaviour() == typeof(PlayerSecondPhase))
            {
                player.behaviourCurrent.Shoot(new Bullet3());
            }

            if (xIsPressed && player.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged))
            {
               player.behaviourCurrent.Shoot(new Bullet2());
            }
        }
    }

    private void ShootTier3()
    {
        if ((player.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || player.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage)) && !isCompressed)
        {
            if (zIsPressed)
            {
                player.behaviourCurrent.Shoot(new Bullet1());
            }

            if (xIsPressed)
            {
                player.behaviourCurrent.Shoot(new Bullet3());
            }

            if (cIsPressed && player.GetCurrentBehaviour() == typeof(PlayerThirdPhase))
            {
                player.behaviourCurrent.Shoot(new Bullet5());
            }

            if (cIsPressed && player.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage))
            {
                player.behaviourCurrent.Shoot(new Bullet4());
            }
		}
    }

    private void CheatPoints()
    {
        if (Input.GetKey(KeyCode.P))
        {
            player.Points += 20;
        }
    }
    private void Shrink()
    {
		if (Input.GetKey(KeyCode.LeftShift) && (player.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || player.GetCurrentBehaviour() == typeof(PlayerSecondPhase)))
		{
			Debug.Log("Shrink");
			playerCompressed.Shrink(true);
            isCompressed = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) && (player.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || player.GetCurrentBehaviour() == typeof(PlayerSecondPhase)))
		{
			playerCompressed.Shrink(false);
            isCompressed = false;
		}
	}

    private IPlayerBehaviour GetBehaviour<T>() where T : IPlayerBehaviour
    {
        var type = typeof(T);
        return (T)player.behavioursMap[type];
    }

    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.Z) && !xIsPressed && !cIsPressed)
        {
            zIsPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Z)) {
            zIsPressed = false;
        }

		if (Input.GetKey(KeyCode.X) && !zIsPressed && !cIsPressed)
		{
			xIsPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.X))
		{
			xIsPressed = false;
		}

		if (Input.GetKey(KeyCode.C) && !zIsPressed && !xIsPressed)
		{
			cIsPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			cIsPressed = false;
		}
	}
}
