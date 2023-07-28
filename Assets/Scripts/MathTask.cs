using TMPro;
using UnityEngine;

public class MathTask : MonoBehaviour
{
	public int Result { get; set; }
	TextMeshPro text;
	[SerializeField] public Transform QuestionMarkPosition;

	private void OnEnable()
	{
		Answer.OnAnswerDestroyed += Die;
	}

	private void OnDisable()
	{
		Answer.OnAnswerDestroyed -= Die;
	}

	void Start()
    {
		text = GetComponentInChildren<TextMeshPro>();
		MathTaskGenerator taskGenerator = new MathTaskGenerator();
		taskGenerator.Init(text);
		taskGenerator.CreateTask();
		SetResult(taskGenerator.Result);
		Destroy(gameObject, 10);
	}

	private void FixedUpdate()
	{
		Move();
	}

	void SetResult(int result)
	{
		Result = result;
	}
	private void Move()
	{
		transform.Translate(Vector3.down * 5f * Time.deltaTime, Space.World);
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
