using UnityEngine;

public class HealthPack : MonoBehaviour
{
    float directionX = 0;
    float sideSpeed = 4f;

    void Start()
    {
        detrmineDirectionX();
    }
    void Update()
    {
        ConstantMoveToSide();
    }

    void ConstantMoveToSide()
    {
        transform.Translate(directionX*Time.deltaTime*sideSpeed, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
			playerHealth.GetHealth(50);
            Destroy(gameObject);
        }
    }


	void detrmineDirectionX()
    {
        if (transform.position.x <= 0)
        {
            directionX = 1;
        }
        else
        {
            directionX = -1;
        }
    }
}
