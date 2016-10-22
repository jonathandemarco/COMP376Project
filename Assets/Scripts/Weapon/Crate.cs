using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {
	
	public float delta = 1.5f;  // Amount to move left and right from the start point
	public float speed = 2.0f; 
	private Vector3 startPos;

	bool grounded;

	void Start(){
		grounded = false;
		startPos = transform.position;
	}

	void Update () {
		if (!grounded) {
			Vector3 v = startPos;
			v.x += delta * Mathf.Sin (Time.time * speed);
			v.y -= Time.time * 5;
			transform.position = v;
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("Terrain")) {
			grounded = true;
		}
	}
}
