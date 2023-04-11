using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    
    TextMeshPro text;
    private ParticleSystem particles;

    private float timer = 1f;
    private float destructionTimer = 10f;
    private bool isCorrect;

    public bool IsDestroyed { get; private set; } = false;

    private int result;

    void Start()
    {
     particles= FindObjectOfType<ParticleSystem>();
	}

    void Update()
    {
        destructionTimer-=Time.deltaTime;
        if (destructionTimer <= 0) 
        {
            IsDestroyed= true;
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
                if(isCorrect)
                {
                    Debug.Log("YOU ARE RIGHT, BITCH");
                }
                else
                {
                    Debug.Log("YOU ARE DUMB AS FUCK");
                }

                IsDestroyed = true;
            }
        }
	}

	private void OnDestroy()
	{
        if (isCorrect)
        {
			particles.transform.position = transform.position;
			particles.Play();
		}
	}

	private void Move()
	{
		transform.Translate(Vector3.down * 5f * Time.deltaTime, Space.World);
	}
}
