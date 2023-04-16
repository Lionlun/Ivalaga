using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	private GameObject player;
	private Vector3 direction = Vector3.zero;
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");

	}

    // Update is called once per frame
    void Update()
    {
		direction = player.transform.position - transform.position;
		float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0,0,rotation+-90);
	}
}
