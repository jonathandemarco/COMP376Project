using UnityEngine;
using System.Collections;

public class Vacuum : Weapon {
	GravitySource gSource;
	public float maxSuction;
	public float suctionIncrement;
	public GameObject suctionPrefab;
	public GameObject suctionEffect;

	public override void Start()
	{
		base.Start ();
		transform.localRotation = Quaternion.Euler (90, 0, 0);
//		renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

		gSource = new GravitySource (new GameObject("GravityCenter"), 0);
		gSource.source.transform.SetParent (transform);
		gSource.source.transform.rotation = Quaternion.Euler (0, 0, 0);
		gSource.source.transform.localPosition = new Vector3 (1, 0, 0);

		gSource.forceCalculation = delegate(GravitySource gS, Transform g) {
			Vector3 diff = (gS.source.transform.position - g.transform.position);
			float angle = Vector3.Angle(gS.source.transform.parent.up, (g.position - gS.source.transform.position).normalized) * Mathf.Deg2Rad;
			Vector3 force = gS.mass * diff.normalized / (1.0f + diff.sqrMagnitude + Mathf.Pow(4 * angle, 4));

			Bounds b = new Bounds();
			if(g.gameObject.GetComponent<Renderer>())
				b = g.gameObject.GetComponent<Renderer>().bounds;
			
			Renderer[] r = g.gameObject.GetComponentsInChildren<Renderer>();
			foreach(Renderer rend in r)
			{
				b.Encapsulate(rend.bounds);
			}

			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, b.center);
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.min.x, b.center.y, b.center.z));
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.center.x, b.min.y, b.center.z));
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.center.x, b.center.y, b.min.z));
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.max.x, b.center.y, b.center.z));
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.center.x, b.max.y, b.center.z));
			g.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, new Vector3(b.center.x, b.center.y, b.max.z));
		};

		GravitationalForces.gravityCenters.Add (gSource);
	}
		
	public override void PressAttack(InputSystem button) {
		display ();
		suctionEffect = Instantiate (suctionPrefab, transform.position + transform.forward, Quaternion.identity, transform) as GameObject;
	}

	public override void ReleaseAttack (InputSystem button) 
	{
		Debug.Log ("Released");

		hide ();
		Destroy (suctionEffect);
		gSource.mass = 0;
	}

	public override void HoldAttack (InputSystem button)
	{
		if(gSource.mass < maxSuction)
			gSource.mass += suctionIncrement * Time.deltaTime;
	}

	public void OnDestroy()
	{
		GravitationalForces.gravityCenters.Remove (gSource);
	}
}
