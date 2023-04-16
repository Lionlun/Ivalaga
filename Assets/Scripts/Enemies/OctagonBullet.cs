using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonBullet : MonoBehaviour
{
	private float bulletSpeed = 8f;

	void Start()
    {
    }


    void Update()
    {
		BulletMovement();
		Destroy(gameObject, 4);
	}

	void BulletMovement()
	{
		transform.position += transform.up * bulletSpeed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player player = collision.gameObject.GetComponentInParent<Player>();
			player.OwnDamage(10);
			player.TakePoints(10);
			Destroy(gameObject);
		}
	}
}
