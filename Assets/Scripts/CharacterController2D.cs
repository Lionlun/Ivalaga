using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] float speed = 20;
	[SerializeField]float acceleration = 75;
	[SerializeField]float deceleration = 70;
    private BoxCollider2D boxCollider;
	private Vector2 velocity;

	private bool isMovingRight;
	private bool isMovingLeft;
	private float changeDirectionTimer = 2f;

	private int pushBackDelay = 200;

	float moveInputX;
	float moveInputY;
	private void Awake()
	{
		boxCollider = GetComponent<BoxCollider2D>();
	}
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		CheckInput();
	}

	private void FixedUpdate()
	{
		Movement();
	}

	private async void Movement()
	{
		moveInputX = Input.GetAxisRaw("Horizontal");
		moveInputY = Input.GetAxisRaw("Vertical");

		
		rb.AddForce(new Vector2(moveInputX, moveInputY)*speed, ForceMode2D.Force);



		if (moveInputX == 0 && isMovingRight)
		{
			await PushLeft();
			isMovingRight= false;
		}
		if (moveInputX == 0 && isMovingLeft)
		{
			await PushRight();
			isMovingLeft = false;
		}
	}


	private async Task PushLeft()
	{
		rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
		//rb.AddForce(-rb.velocity*2, ForceMode2D.Impulse);
		await Task.Delay(pushBackDelay);
	}

	private async Task PushRight()
	{
		rb.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
		//rb.AddForce(-rb.velocity*2, ForceMode2D.Impulse);
		await Task.Delay(pushBackDelay);

	}
	private async Task Detract()
	{
			velocity.x -= 4f;

			transform.Translate(velocity*4 * Time.deltaTime);
			await Task.Delay(300);
		
			Debug.Log("SLIIIIIIIIIIIIIIIIDe");

		velocity.x = 0;
		await Task.Yield();
	}

	private async Task DetractBack()
	{
		velocity.x += 4f;

		transform.Translate(velocity*4 * Time.deltaTime);
		await Task.Delay(300);

		Debug.Log("SLIIIIIIIIIIIIIIIIDe");

		velocity.x = 0;
		await Task.Yield();
	}

	private async void CheckInput()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow)) 
		{
			isMovingRight= true;
		}

		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			await TimerForDirection();
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			isMovingLeft = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			await TimerForDirection();
		}
	}

	private async Task TimerForDirection()
	{
		while (changeDirectionTimer != 0)
		{
			changeDirectionTimer-=Time.deltaTime;
			await Task.Yield();
		}
		isMovingRight= false;
		isMovingLeft = false;
		await Task.Yield();
	}
}
