using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossRay : MonoBehaviour
{
    private bool isAttackPhase;
	Boss boss;
	Vector3 shootingPointOffset = new Vector3(0, -2f);

	private void Start()
	{
		boss = FindObjectOfType<Boss>();
	}
	private void Update()
	{
		transform.position = boss.transform.position + shootingPointOffset;
	}
	public void SetAttackPhase()
	{
		isAttackPhase = true;
	}

	public void Destroy()
	{
		if (this.gameObject!= null)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isAttackPhase && gameObject != null)
		{
			if (collision.gameObject.tag == "Player")
			{
				Player player = collision.gameObject.GetComponentInParent<Player>();
				player.TakeDamage(150);
				player.TakePoints(300);
				isAttackPhase = false;
			}
		}
	}
}
