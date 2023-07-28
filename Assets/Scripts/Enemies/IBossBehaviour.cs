using UnityEngine;

public abstract class IBossBehaviour: ScriptableObject
{
    public virtual void Enter() { }
    public virtual void Exit() { }
	public virtual void Update() { }
	public abstract void Attack();
}
