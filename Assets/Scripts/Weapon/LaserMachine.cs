using UnityEngine;
using System.Collections;

public class LaserMachine : Weapon {
	
	public override void OnCollisionEnter(Collision c)
	{
		Collider col = c.collider;

		if (col.gameObject.tag != "LaserLine" && col.gameObject.layer != LayerMask.NameToLayer("Terrain")) {
			gameObject.transform.position = col.gameObject.transform.position - gameObject.transform.forward;
		}

	}

}
