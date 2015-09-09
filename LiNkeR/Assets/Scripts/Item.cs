using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	
	protected GameObject owner;
	protected float globalCooldown = 1f;
	protected float globalCounter = 0.0f;
	protected bool canAttackOwner;

	public virtual void Activate()
	{

	}

	public virtual void OnCollisionEnter2D()
	{

	}

	public void SetOwner(GameObject o)
	{
		owner = o;
	}

}
