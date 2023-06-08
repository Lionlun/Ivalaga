using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSnake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void FixedUpdate()
	{
		float sin = Mathf.Sin(transform.position.y / 1.5f) / 10;
		transform.Translate(new Vector2(0, 0.1f));
		float angle = Mathf.Atan2(sin, 0.1f) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
