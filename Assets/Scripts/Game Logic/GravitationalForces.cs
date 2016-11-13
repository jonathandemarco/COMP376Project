using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravitySource {
	public GameObject source;
	public float mass;

	public GravitySource(GameObject source, float mass)
	{
		this.source = source;
		this.mass = mass;
	}
}

public class GravitationalForces : MonoBehaviour {
	public static List<GravitySource> gravityCenters = new List<GravitySource> ();

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		foreach (GravitySource g in gravityCenters) {
			Vector3 diff = (g.source.transform.position - transform.position);
			Vector3 force = g.mass * diff.normalized / (1.0f + diff.sqrMagnitude);
			GetComponent<Rigidbody> ().AddForce (force);
		}
	}
}
