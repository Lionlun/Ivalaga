using UnityEngine;

public class SinOscilation : MonoBehaviour
{
	static private float sinOffsetY;
    private float oscillationDamper = 40;
    void FixedUpdate()
    {
        sinOffsetY = Mathf.Sin(transform.position.x)/oscillationDamper;
        transform.position += new Vector3(0, sinOffsetY);
    }
}
