using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalProjectile : MonoBehaviour
{
    float speed = 5f;
    void Start()
    {
        Destroy(gameObject, 8f);
    }
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime; 
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Player player = collision.gameObject.GetComponentInParent<Player>();
			player.TakeDamage(10);

			//particleEffect.transform.position = transform.position;
			//particleEffect.Play();

			Destroy(gameObject);
		}
	}
	
}
