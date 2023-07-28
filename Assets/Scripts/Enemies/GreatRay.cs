using System.Threading.Tasks;
using UnityEngine;

public class GreatRay : MonoBehaviour
{
    private bool isAttackPhase;
  
    public async void SetAttackPhase()
    {
        isAttackPhase = true;
        await Task.Delay(300);
        Destroy(gameObject);
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
        if(isAttackPhase && gameObject!=null)
        {
			if (collision.gameObject.tag == "Player")
			{
				Player player = collision.gameObject.GetComponentInParent<Player>();
				Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
				playerHealth.TakeDamage(150);
				player.TakePoints(300);
				isAttackPhase = false;
			}
		}
	}
}
