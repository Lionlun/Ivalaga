using System.Threading.Tasks;
using UnityEngine;

public class HorizontalProjectile : MonoBehaviour
{
    float speed = 15f;
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] GameObject projectileAlert;

    void Start()
    {
        var newAlert = Instantiate(projectileAlert, new Vector3(-16, transform.position.y), Quaternion.identity);
        Destroy(newAlert, 2);
        Destroy(gameObject, 8f);
	}
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime; 
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
			playerHealth.TakeDamage(10);
            var effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
			effect.Play();

			Destroy(gameObject);
		}
	}
	
}
