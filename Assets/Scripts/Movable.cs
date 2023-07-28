using System.Collections;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
	[SerializeField] protected Rigidbody2D Rigidbody;

    Vector3 startDownMovement = new Vector3(0f, -10f, 0f);
    
	private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
	}
	public virtual IEnumerator EnemyMovementPattern()
    {
        Rigidbody.AddForce(startDownMovement, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.5f);
    }
}
