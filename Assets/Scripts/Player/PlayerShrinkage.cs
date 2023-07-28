using UnityEngine;

public class PlayerShrinkage : MonoBehaviour
{
	public Animator Animator;
	public Player Player;

	public void Start()
	{
		Animator = GetComponent<Animator>();
	}
	public void Shrink()
    {
        Animator.SetBool("IsCompressed", true);
    }

	public void Unshrink()
	{
		Animator.SetBool("IsCompressed", false);
	}
}
