using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerGun : MonoBehaviour
{
	public Player Player;

	private float timerForNextAttack = 0.1f;
	private float cooldown = 0.1f;
	private int bulletOwnDamage;
	[SerializeField] Transform ShootingPoint;
    
    public void Shoot(PlayerBulletBase playerBulletType)
    {
		if (timerForNextAttack > 0)
		{
			timerForNextAttack -= Time.deltaTime;
		}
		else if (timerForNextAttack <= 0)
		{
			var shootOffset = new Vector3(Random.Range(-0.5f, 0.4f), Random.Range(0, 0.5f));
			var randomAngle = Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360.0f));
			var bullet = Instantiate(playerBulletType, ShootingPoint.position + shootOffset, randomAngle);

			bulletOwnDamage = bullet.BulletOwnDamage;

			Player.TakeDamageFromOwnBullet(bulletOwnDamage);
			timerForNextAttack = cooldown;
		}
    }
}
