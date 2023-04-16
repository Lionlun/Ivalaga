using UnityEngine;

public class PlayerCompressedState : MonoBehaviour
{
	public Animator animator;
	public Player player;

	public void Start()
	{
		animator = GetComponent<Animator>();
	}
	public void Shrink(bool isPressed)
    {
        animator.SetBool("IsCompressed", isPressed);
    }
}
