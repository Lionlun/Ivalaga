using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
	public bool IsDestroyed { get; private set; }

    public static event Action OnAnswerDestroyed;

	TextMeshPro text;
    [SerializeField] ParticleSystem particles;

	Player player;

    float timer = 1f;
    float destructionTimer = 10f;
    bool isCorrect;

    private int result;

	private void OnEnable()
	{
		OnAnswerDestroyed += Die;
	}
	private void OnDisable()
	{
		OnAnswerDestroyed -= Die;
	}
	void Start()
    {
		player = FindObjectOfType<Player>();
	}

    void Update()
    {
        destructionTimer-=Time.deltaTime;
        if (destructionTimer <= 0) 
        {
            IsDestroyed = true;
			OnAnswerDestroyed?.Invoke();

			Destroy(gameObject);
        }
    }
	private void FixedUpdate()
	{
        Move();
	}
	public void SetResult(int result)
    {
        this.result = result;
    }

	public void SetCorrect(bool isCorrect)
	{
       this.isCorrect = isCorrect;
	}
	public void ShowResult()
    {
		text = GetComponentInChildren<TextMeshPro>();
		text.text = result.ToString();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
        {
            timer-=Time.deltaTime;

            if (timer <=0)
            {
				var playerHealth = collision.gameObject.GetComponentInParent<Health>();

				var effect = Instantiate(particles, transform.position, Quaternion.identity);
				effect.Play();

				if (isCorrect)
                {
					playerHealth.GetHealth(50);
                }
                else
                {
					playerHealth.TakeDamage(100);
                }

                IsDestroyed = true;
				OnAnswerDestroyed?.Invoke();
			}
        }
	}

	private void Move()
	{
		transform.Translate(Vector3.down * 5f * Time.deltaTime, Space.World);
	}

    private void Die()
    {
        Destroy(gameObject);
    }
}
