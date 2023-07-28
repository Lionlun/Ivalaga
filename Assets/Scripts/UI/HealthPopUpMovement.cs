using UnityEngine;

public class HealthPopUpMovement : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 2f * Time.deltaTime, 0);
    }
}
