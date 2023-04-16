using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBullet: MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private float force = 650f;
    private Vector3 direction= Vector3.zero;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
		direction = player.transform.position - transform.position;
 
		rb.AddForce(new Vector2(direction.x, direction.y).normalized * force);
	}

}
