using UnityEngine;
using System.Collections;

public class HostileTerrain : MonoBehaviour, MessagePassing
{
	public float damage;

	public virtual float calculateDamage()
	{
		return damage;
	}

	public virtual void OnCollisionEnter(Collision c){
		Collider col = c.collider;
		MessagePassingHelper.passMessageOnCollision (this, col);
	}

	void MessagePassing.collisionWith(Collider c)
	{
	}
}

