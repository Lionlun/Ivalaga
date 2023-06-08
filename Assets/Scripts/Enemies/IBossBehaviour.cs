using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBossBehaviour: ScriptableObject
{
    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void Attack();
	public abstract void Move();
	public abstract void Update();
    
}
