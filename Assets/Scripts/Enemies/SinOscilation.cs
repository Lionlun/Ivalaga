using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinOscilation : MonoBehaviour
{
	static private float sinOffsetY;
	
	void Start()
    {
        
    }

    void FixedUpdate()
    {
        sinOffsetY = Mathf.Sin(transform.position.x)/40;
        transform.position += new Vector3(0, sinOffsetY);
    }
}
