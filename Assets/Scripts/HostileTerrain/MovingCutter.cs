using UnityEngine;
using System.Collections;

public class MovingCutter : HostileTerrain {

	public float speed = 15;

	void Start() {
		// Initial Velocity
		damage = 25;
		GetComponent<Rigidbody>().velocity = Vector3.right * speed;
	}

	void FixedUpdate(){
		if (GetComponent<Rigidbody> ().velocity.y > 0)
			GetComponent<Rigidbody> ().velocity = new Vector3(GetComponent<Rigidbody> ().velocity.x, 0, GetComponent<Rigidbody> ().velocity.z);
	}

	override public void OnCollisionEnter(Collision c){
		base.OnCollisionEnter (c);

		Collider col = c.collider;
		if (col.gameObject.layer == LayerMask.NameToLayer ("Terrain"))
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * speed;
	}
}
