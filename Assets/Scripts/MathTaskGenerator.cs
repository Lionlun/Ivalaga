using TMPro;
using UnityEngine;

public class MathTaskGenerator
{
	public enum Operators
	{
		Plus = 0,
		Minus = 1,
		Multiply = 2,
		Divide = 3,
	}

	public int Result { get; set; }
	TextMeshPro text;
	private int number1, number2, number3;
	private int divider;

	public void Init(TextMeshPro text)
	{
		this.text = text;
	}

	public void CreateTask()
	{
		SetRandomNumbers();

		Operators randomOperator = RandomOperator();

		switch (randomOperator)
		{
			case Operators.Plus:
				CreatePlusTask();
				break;

			case Operators.Minus:
				CreateMinusTask();
				break;

			case Operators.Multiply:
				CreateMultiplyTask();
				break;

			case Operators.Divide:
				CreateDivideTask();
				break;
		}
	}

	void SetRandomNumbers()
	{
		number1 = Random.Range(2, 99);
		number2 = Random.Range(2, 99);
		number3 = Random.Range(2, 10);
		divider = Random.Range(2, 10);
	}

	private Operators RandomOperator()
	{
		var v = Operators.GetValues(typeof(Operators));
		return (Operators)v.GetValue(Random.Range(0, 4));
	}

	void CreatePlusTask()
	{
		text.text = number1.ToString() + " + " + number2 + " + " + number3 + " = ?";
		Result = number1 + number2 + number3;
	}
	void CreateMinusTask()
	{
		if (number1 > number2)
		{
			text.text = number1.ToString() + " - " + number2 + " = ?";
			Result = number1 - number2;
		}

		else
		{
			text.text = number2.ToString() + " - " + number1 + " = ?";
			Result = number2 - number1;
		}
	}
	void CreateMultiplyTask()
	{
		text.text = number1.ToString() + " * " + (number2 / 10) + " = ?";
		Result = number1 * (number2 / 10);
	}
	void CreateDivideTask()
	{
		while (number1 % divider != 0 && divider < number1)
		{
			divider++;
		}

		if (number1 > divider)
		{
			text.text = number1.ToString() + " / " + divider + " = ?";
			Result = number1 / divider;
		}

		else
		{
			text.text = number1.ToString() + " * " + (number2 / 10) + " = ?";
			Result = number1 * (number2 / 10);
		}
	}
}
