using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBase : MonoBehaviour
{
    private float bulletSpeed = 15f;
    private float rotationValue;

	[SerializeField] protected ParticleSystem particleEffectPrefab;
	[SerializeField] public float timerForNextAttack = 0.1f;
	[SerializeField] public float cooldown = 0.1f;
	public int BulletOwnDamage { get; protected set; }
 
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
