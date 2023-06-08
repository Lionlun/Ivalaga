using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonBullet : MonoBehaviour
{
	public float angle;

	void Start()
    {
		Destroy(gameObject, 4);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player player = collision.gameObject.GetComponentInParent<Player>();
			player.TakeDamage(10);
			player.TakePoints(20);
			Destroy(gameObject);
		}
	}
}
