using UnityEngine;

public abstract class EnemyBaseClass : MonoBehaviour
{
    protected abstract void EnemyAttack();

	public virtual void Die()
    {
		GlobalEvents.SendEnemyKilled();
		Destroy(gameObject);
    }
}
