using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float bulletSpeed = 12f;
    private float rotationValue;
    

    private void Start()
    {
        rotationValue = Random.Range(-2f, 2f);
    }
    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        
        Destroy(gameObject, 2);
    }

    void BulletMovement()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, rotationValue));
    }

   
}
