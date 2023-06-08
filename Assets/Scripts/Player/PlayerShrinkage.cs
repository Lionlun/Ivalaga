using UnityEngine;

public class PlayerShrinkage : MonoBehaviour
{
	public Animator animator;
	public Player player;

	private PlayerSecondPhase playerSecondPhase;
	private PlayerThirdPhase playerThirdPhase;

	public void Start()
	{
		animator = GetComponent<Animator>();
	}
	public void Shrink()
    {
        animator.SetBool("IsCompressed", true);
    }

	public void Unshrink()
	{
		animator.SetBool("IsCompressed", false);
	}
}
