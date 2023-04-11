using TMPro;
using UnityEngine;

public class MathTask : MonoBehaviour
{
	private TextMeshPro text;

	private float oppacityLevel = 1f;

	float destructionTimer = 10f;

	public int Result { get; set; }

	private bool isPlus;
	private bool isMinus;
	private bool isDivide;
	private bool isMultiply;
	private bool[] operators;
	private int divider;

	void Start()
    {
		operators = new bool[4] { isPlus, isMinus, isMultiply, isDivide };
		text = GetComponentInChildren<TextMeshPro>();
		CreateTask();
	}

    void Update()
    {
		text.color = new Color(1f, 0.2f, 0.2f, oppacityLevel);

		if (destructionTimer <= 0) Destroy(gameObject);
	}

	private void FixedUpdate()
	{
		Move();
		oppacityLevel -= 0.001f;
		destructionTimer-= Time.deltaTime;
	}

	private void CreateTask()
	{
		var number1 = Random.Range(2, 99);
		var number2 = Random.Range(2, 99);
		var number3 = Random.Range(2, 10);

		divider = number2 / 10;
		if (divider == 0 || divider == 1)
		{
			divider = 2;
		}

		operators[Random.Range(0, operators.Length)] = true;

		Operators randomOperator = RandomOperator();
		switch (randomOperator)
		{
			case Operators.Plus:
				text.text = number1.ToString() + " + " + number2 + " + " + number3 + " = ?";
				Result = number1 + number2 + number3;
				break;

			case Operators.Minus:
				if(number1 > number2)
				{
					text.text = number1.ToString() + " - " + number2 + " = ?";
					Result = number1 - number2;
				}
				else
				{
					text.text = number2.ToString() + " - " + number1 + " = ?";
					Result = number2 - number1;
				}
				
				break;

			case Operators.Multiply:
				text.text = number1.ToString() + " * " + (number2/10) + " = ?";
				Result = number1 * (number2/10);
				break;

			
			case Operators.Divide:
				if (number1 > divider && number1 % divider == 0)
				{
					text.text = number1.ToString() + " / " + divider + " = ?";
					Result = number1 / divider;
				}
				else if(number1 < divider && divider % number1 == 0)
				{
					text.text = divider.ToString() + " / " + divider + " = ?";
					Result = divider / number1;
				}
				else
				{
					text.text = number1.ToString() + " * " + (number2 / 10) + " = ?";
					Result = number1 * (number2 / 10);
				}

				break;
		}
	}
	private Operators RandomOperator()
	{
		var v = Operators.GetValues(typeof(Operators));
		return (Operators)v.GetValue(Random.Range(0, 4));
	}
	public enum Operators
	{
		Plus = 0,
		Minus = 1,
		Multiply = 2,
		Divide = 3,
	}
	private void Move()
	{
		transform.Translate(Vector3.down * 5f * Time.deltaTime, Space.World);
	}
}
