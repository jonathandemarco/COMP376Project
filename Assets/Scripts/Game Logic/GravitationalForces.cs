using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravitySource {
	public GameObject source;
	public float mass;
	public delegate Vector3 CalculateForce(GravitySource gS, Transform g);
	public CalculateForce forceCalculation;

	public GravitySource(GameObject source, float mass)
	{
		this.source = source;
		this.mass = mass;
		forceCalculation = defaultForceCalculation;
	}

	public static Vector3 defaultForceCalculation(GravitySource gS, Transform g)
	{
		Vector3 diff = (gS.source.transform.position - g.transform.position);
		return gS.mass * diff.normalized / (1.0f + diff.sqrMagnitude);
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
			if (!isAncestor (g.source.transform))
			{	
				GetComponent<Rigidbody> ().AddForce (g.forceCalculation(g, transform));
			}
		}
	}

	public bool isAncestor(Transform t)
	{
		while (t.parent != null && t.parent != transform) {
			t = t.parent;
		}

		return t.parent != null;
	}
}
