using UnityEngine;
using System.Collections;

public class MovingCutter : MonoBehaviour {

	public float speed = 15;
	public int damage = 25;

	void Start() {
		// Initial Velocity
		GetComponent<Rigidbody>().velocity = Vector3.right * speed;
	}

	void FixedUpdate(){
		if (GetComponent<Rigidbody> ().velocity.y > 0)
			GetComponent<Rigidbody> ().velocity = new Vector3(GetComponent<Rigidbody> ().velocity.x, 0, GetComponent<Rigidbody> ().velocity.z);
	}

	void OnCollisionEnter(Collision c){
		Collider col = c.collider;
		if (col.gameObject.layer == LayerMask.NameToLayer ("Terrain"))
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * speed;
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Vector3 direction = col.transform.position - transform.position;
			col.gameObject.GetComponent<PlayerManager> ().takeDamage (damage,direction);
		}
	}
}
