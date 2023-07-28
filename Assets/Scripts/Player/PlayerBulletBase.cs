using UnityEngine;

public class PlayerBulletBase : MonoBehaviour
{
	[SerializeField] public int BulletOwnDamage;
	[SerializeField] protected ParticleSystem ParticleEffectPrefab;

	private float bulletSpeed = 15f;
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
}
