using UnityEngine;
using System.Collections;

public class LaserMachine : Weapon {

	private Transform collidedObject;
	private Vector3 collisionPoint;

	public override void Update(){
		if (collidedObject != null) {
			transform.position = collidedObject.position - collisionPoint;
		}

	}

	public override void OnCollisionEnter(Collision c)
	{
		Collider col = c.collider;  
		if (col.gameObject.layer != LayerMask.NameToLayer ("Terrain")) {			
			collidedObject = col.gameObject.transform;
			collisionPoint = collidedObject.position - c.contacts [0].point;
			gameObject.GetComponent<Collider>().enabled = false;
		}

	}

}
