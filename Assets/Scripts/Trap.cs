using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public int damage = 25;

	void OnCollisionEnter(Collision c){
		Collider col = c.collider; 
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Vector3 direction = col.transform.position - transform.position;
			col.gameObject.GetComponent<PlayerManager> ().takeDamage (damage,direction);
		}
	}
}
