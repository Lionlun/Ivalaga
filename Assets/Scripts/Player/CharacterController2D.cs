using System.Threading.Tasks;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	private AudioSource sound;
	[SerializeField] private AudioClip dashSound;

	Animator animator;

	[SerializeField] float speed = 20;
	[SerializeField] float acceleration;
	[SerializeField] float deceleration;

	[SerializeField] PlayerDashEffect dashEffect;

	private Vector2 velocity;

	float moveInputX;
	float moveInputY;

	private float dashCooldown = 0;
	private float dashCooldownRefresh = 2f;

	[SerializeField] DashBar dashBar;

	private void Awake()
	{
		dashCooldown = dashCooldownRefresh;
		animator = GetComponent<Animator>();
		dashBar.SetMaxValue(dashCooldownRefresh);
		sound = GetComponent<AudioSource>();
	}
    void Update()
    {
		Movement();

		if (dashCooldown < dashCooldownRefresh) //сделать нормальный, читабельный кулдаун
		{
			dashBar.gameObject.SetActive(true); //возможно переместить управление юай деша в другой класс
			dashCooldown += Time.deltaTime;
			dashBar.DashUpdate(dashCooldown);
		}
		if (dashCooldown >= dashCooldownRefresh)
		{
			dashBar.gameObject.SetActive(false); //возможно переместить управление юай деша в другой класс
		}
	}

	public async void Dash()
	{
		if (dashCooldown >= dashCooldownRefresh)
		{
			var vectorX = Input.GetAxisRaw("Horizontal");
			var vectorY = Input.GetAxisRaw("Vertical");
			var targetVector = transform.position + new Vector3(vectorX * 6, vectorY * 6);
			var dashLength = 40;
			var dashSpeed = 20f;

			sound.clip = dashSound;
			sound.Play();

			for (int i = 0; i < dashLength; i++)
			{
				Instantiate(dashEffect, transform.position, Quaternion.identity);
				transform.position = Vector3.MoveTowards(transform.position, targetVector, dashSpeed * Time.deltaTime);
				await Task.Yield();
			}
			dashCooldown = 0;
		}
	}
	private void Movement()
	{
		moveInputX = Input.GetAxisRaw("Horizontal");
		moveInputY = Input.GetAxisRaw("Vertical");

		if (moveInputX != 0)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInputX, acceleration * Time.deltaTime);
		}
		if (moveInputX == 0)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
		}

		if (moveInputY != 0)
		{
			velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputY, acceleration * Time.deltaTime);
		}
		else
		{
			velocity.y = Mathf.MoveTowards(velocity.y, 0, deceleration * Time.deltaTime);
		}

		transform.Translate(velocity * Time.deltaTime);
	}
}
