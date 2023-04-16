using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet4 : MonoBehaviour, IPlayerBullet
{
    [SerializeField] BulletBehaviour BulletPrefab;
    [SerializeField] Transform ShootingPoint;
    [SerializeField] Player Player;

	[SerializeField] float timerForNextAttack = 0.1f;
	[SerializeField] float cooldown = 0.1f;

	private int bulletOwnDamage = 40;

    public void Shoot()
    {
		if (timerForNextAttack > 0)
		{
			timerForNextAttack -= Time.deltaTime;
		}
		else if (timerForNextAttack <= 0)
		{
			Instantiate(BulletPrefab, ShootingPoint.position + new Vector3(Random.Range(-0.5f, 0.4f), Random.Range(0, 0.5f)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360.0f)));
			Player.OwnDamage(bulletOwnDamage);
			timerForNextAttack = cooldown;
		}
	}
}
