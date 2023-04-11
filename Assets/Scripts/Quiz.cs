using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
	[SerializeField] private Answer answer;
	[SerializeField] MathTask mathTask;
	
	private bool isFirstDestroyed = false;
	private bool isSecondDestroyed = false;
	private bool isThirdDestroyed = false;
	private float wait;

	void Start()
    {
        StartCoroutine(CreateQuiz());
    }
	private IEnumerator CreateQuiz()
    {
		var task = CreateTask();

		yield return new WaitForSeconds(0.5f);

		List<Vector2> answersLocation = GetAnswersLocation();

		int randomDeviation = Random.Range(-9, 9);
		if (randomDeviation == 0) randomDeviation += 1;
		int randomDeviation2 = Random.Range(-9, 9);
		if (randomDeviation2 == 0) randomDeviation2 -= 1;

		var answerResult = CreateCorrectAnswer(answersLocation[0], task.Result);
		var wrongAnswer1 = CreateWrongAnswer(answersLocation[1], task.Result);
		var wrongAnswer2 = CreateWrongAnswer(answersLocation[2], task.Result);
		
		while (!isFirstDestroyed && !isSecondDestroyed && !isThirdDestroyed)
		{
			isFirstDestroyed = answerResult.IsDestroyed;
			isSecondDestroyed = wrongAnswer1.IsDestroyed;
			isThirdDestroyed = wrongAnswer2.IsDestroyed;
			yield return null;
		}
		
		isFirstDestroyed = false;
		isSecondDestroyed = false;
		isThirdDestroyed = false;

		if(answerResult != null) Destroy(answerResult.gameObject);
		if (wrongAnswer1 != null) Destroy(wrongAnswer1.gameObject);
		if(wrongAnswer2 != null) Destroy(wrongAnswer2.gameObject);
		if (task != null) Destroy(task.gameObject);

		yield return null;
		StartCoroutine(CreateQuiz());
    }

	private MathTask CreateTask()
	{
		var taskPosition = new Vector2(Random.Range(-2, 10), 10);
		var task = Instantiate(mathTask, taskPosition, Quaternion.identity);
		return task;
	}

	private List<Vector2> GetAnswersLocation()
	{
		List<Vector2> answersPosition= new List<Vector2>();

		var answerRandomPosition = new Vector2(Random.Range(-11, 12), Random.Range(13, 22));
		var secondAnswerRandomPosition = new Vector2(Random.Range(-11, 12), Random.Range(13, 22));
		var thirdAnswerRandomPosition = new Vector2(Random.Range(-11, 12), Random.Range(13, 22));

		CheckOverlap(ref answerRandomPosition, ref secondAnswerRandomPosition, ref thirdAnswerRandomPosition);

		answersPosition.Add(answerRandomPosition);
		answersPosition.Add(secondAnswerRandomPosition);
		answersPosition.Add(thirdAnswerRandomPosition);
		
		return answersPosition;
	}

	private void CheckOverlap(ref Vector2 answerRandomPosition, ref Vector2 secondAnswerRandomPosition, ref Vector2 thirdAnswerRandomPosition)
	{
		while (Mathf.Abs(answerRandomPosition.x - secondAnswerRandomPosition.x) < 4) secondAnswerRandomPosition.x -= 1f;

		while (Mathf.Abs(secondAnswerRandomPosition.x - thirdAnswerRandomPosition.x) < 4) secondAnswerRandomPosition.x -= 1f;

		while (Mathf.Abs(thirdAnswerRandomPosition.x - answerRandomPosition.x) < 4) thirdAnswerRandomPosition.x += 1f;

		while (Mathf.Abs(secondAnswerRandomPosition.x - thirdAnswerRandomPosition.x) < 4) thirdAnswerRandomPosition.x += 1f;
	}

	private Answer CreateCorrectAnswer(Vector2 position, int result)
	{
		Answer answerResult = Instantiate(answer, position, Quaternion.identity);
		answerResult.SetResult(result);
		answerResult.SetCorrect(true);
		answerResult.ShowResult();
		return answerResult;
	}

	private Answer CreateWrongAnswer(Vector2 position, int result)
	{
		var randomDeviation = 0;
		while(randomDeviation == 0)
		{
			randomDeviation = Random.Range(-9, 9);
		}

		Answer answerResult = Instantiate(answer, position, Quaternion.identity);
		
		answerResult.SetResult(result + randomDeviation);
		answerResult.SetCorrect(false);
		answerResult.ShowResult();

		return answerResult;
	}
}
