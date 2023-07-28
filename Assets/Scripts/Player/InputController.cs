using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;
    public Player player;
    public PlayerGun gun;
    public Animator animator;
	[SerializeField] CharacterController2D characterController;
	[SerializeField] PlayerShrinkage playerCompressed;

	[SerializeField] PlayerBullet bullet1;
    [SerializeField] PlayerBullet bullet2;
    [SerializeField] PlayerBullet bullet3;
    [SerializeField] PlayerBullet bullet4;
    [SerializeField] PlayerBullet bullet5;


    [SerializeField] ScriptableObject playerFirstPhase;
    [SerializeField] ScriptableObject playerSecondPhase;
    [SerializeField] ScriptableObject playerSecondPhaseDamaged;
    [SerializeField] ScriptableObject playerThirdPhase;
    [SerializeField] ScriptableObject playerThirdPhaseDamaged;

	private bool isCompressed;
    private bool zIsPressed;
    private bool xIsPressed;
    private bool cIsPressed;

	private void Start()
	{
		playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        player = FindObjectOfType<Player>();
		gun = FindObjectOfType<PlayerGun>();
        animator = FindObjectOfType<Animator>();
		characterController = FindObjectOfType<CharacterController2D>();
		playerCompressed = FindObjectOfType<PlayerShrinkage>();

	}

	void Update()
    {
		InputCheck();
		ShootTier1();
        ShootTier2();
        ShootTier3();
        Shrink();
        Dash();
	}

    private void FixedUpdate()
    {
		CheatPoints();
		float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");
    }

	private void ShootTier1()
	{
		if (playerBehaviour.GetCurrentBehaviour() == typeof(PlayerFirstPhase))
        {
			if (Input.GetKey(KeyCode.Z))
			{
				playerBehaviour.behaviourCurrent.Shoot(bullet1);
			}
		}
    }

    private void ShootTier2()
    {
        if ((playerBehaviour.GetCurrentBehaviour() == typeof(PlayerSecondPhase) || playerBehaviour.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged)) && !isCompressed)
        {
            if (zIsPressed)
            {
				playerBehaviour.behaviourCurrent.Shoot(bullet1);
            }

            if (xIsPressed && playerBehaviour.GetCurrentBehaviour() == typeof(PlayerSecondPhase))
            {
                playerBehaviour.behaviourCurrent.Shoot(bullet3);
            }

            if (xIsPressed && playerBehaviour.GetCurrentBehaviour() == typeof(PlayerSecondPhaseDamaged))
            {
               playerBehaviour.behaviourCurrent.Shoot(bullet2);
            }
        }
    }

    private void ShootTier3()
    {
        if ((playerBehaviour.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || playerBehaviour.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage)) && !isCompressed)
        {
            if (zIsPressed)
            {
                playerBehaviour.behaviourCurrent.Shoot(bullet1);
            }

            if (xIsPressed)
            {
                playerBehaviour.behaviourCurrent.Shoot(bullet3);
            }

            if (cIsPressed && playerBehaviour.GetCurrentBehaviour() == typeof(PlayerThirdPhase))
            {
                playerBehaviour.behaviourCurrent.Shoot(bullet5);
            }

            if (cIsPressed && playerBehaviour.GetCurrentBehaviour() == typeof(PlayerThirdPhaseDamage))
            {
                playerBehaviour.behaviourCurrent.Shoot(bullet4);
            }
		}
    }

    private void CheatPoints()
    {
        if (Input.GetKey(KeyCode.P))
        {
            player.GetPoints();
        }
    }
    private void Shrink()
    {
		if (Input.GetKey(KeyCode.LeftShift) && (playerBehaviour.GetCurrentBehaviour() == typeof(PlayerThirdPhase) || playerBehaviour.GetCurrentBehaviour() == typeof(PlayerSecondPhase)))
		{
			playerCompressed.Shrink();
            isCompressed = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			playerCompressed.Unshrink();
            isCompressed = false;
		}
	}

    private IPlayerBehaviour GetBehaviour<T>() where T : IPlayerBehaviour
    {
        var type = typeof(T);
        return (T)playerBehaviour.behavioursMap[type];
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

	private void Dash()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			characterController.Dash();
		}
	}
}
