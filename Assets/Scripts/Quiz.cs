using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
	[SerializeField] Answer answer;
	[SerializeField] MathTask mathTask;

	void Start()
    {
        StartCoroutine(CreateQuiz());
    }

	IEnumerator CreateQuiz()
    {
		var task = CreateTask();

		yield return new WaitForSeconds(0.5f);

		List<Vector2> answersLocation = GetAnswersLocation();

		var answerResult = CreateCorrectAnswer(answersLocation[0], task.Result);
		var wrongAnswer1 = CreateWrongAnswer(answersLocation[1], task.Result);
		var wrongAnswer2 = CreateWrongAnswer(answersLocation[2], task.Result);
		
		while (!answerResult.IsDestroyed && !wrongAnswer1.IsDestroyed && !wrongAnswer2.IsDestroyed)
		{
			yield return null;
		}

		yield return null;
		StartCoroutine(CreateQuiz());
    }

	MathTask CreateTask()
	{
		var taskPosition = new Vector2(Random.Range(-2, 10), 10);
		var task = Instantiate(mathTask, taskPosition, Quaternion.identity);
		return task;
	}

	List<Vector2> GetAnswersLocation()
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

	void CheckOverlap (ref Vector2 answerRandomPosition, ref Vector2 secondAnswerRandomPosition, ref Vector2 thirdAnswerRandomPosition)
	{
		while (Mathf.Abs(answerRandomPosition.x - secondAnswerRandomPosition.x) < 4) secondAnswerRandomPosition.x -= 1f;

		while (Mathf.Abs(secondAnswerRandomPosition.x - thirdAnswerRandomPosition.x) < 4) secondAnswerRandomPosition.x -= 1f;

		while (Mathf.Abs(thirdAnswerRandomPosition.x - answerRandomPosition.x) < 4) thirdAnswerRandomPosition.x += 1f;

		while (Mathf.Abs(secondAnswerRandomPosition.x - thirdAnswerRandomPosition.x) < 4) thirdAnswerRandomPosition.x += 1f;
	}

	Answer CreateCorrectAnswer(Vector2 position, int result)
	{
		Answer answerResult = Instantiate(answer, position, Quaternion.identity);
		answerResult.SetResult(result);
		answerResult.SetCorrect(true);
		answerResult.ShowResult();
		return answerResult;
	}

	Answer CreateWrongAnswer(Vector2 position, int result)
	{
		var randomDeviation = 0;
		while (randomDeviation == 0)
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
