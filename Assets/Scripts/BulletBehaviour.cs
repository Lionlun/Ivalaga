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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyBaseClass enemy = collision.gameObject.GetComponentInParent<EnemyBaseClass>();
            enemy.EnemyTakeDamage(10);

            Destroy(gameObject);
        }
    }
}
