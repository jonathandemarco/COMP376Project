using UnityEngine;
using System.Collections;

public class LaserBoundary : Weapon {
	private LineRenderer lineRenderer;

	public Transform origin;
	public Transform destination;
	public float lineWidth;
	public GameObject SphericalMachine;

	private CapsuleCollider capsule;
	private bool isComplete;

	void Start(){
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;

	}

	// Use this for initialization
	public override void Update () {
		if (isComplete) {
			lineRenderer.SetPosition (0, origin.position);
			lineRenderer.SetPosition (1, destination.position);
			lineRenderer.SetWidth (lineWidth, lineWidth);

			capsule.transform.position = origin.position + (destination.position - origin.position) / 2; // find the mid point
			capsule.transform.LookAt (origin.position);
			capsule.height = (destination.position - origin.position - Vector3.Normalize(destination.position - origin.position) * (SphericalMachine.GetComponent<SphereCollider>().radius * 2 + 0.1f)).magnitude;
		}
	}

	public void RenderLine(){		
		lineRenderer.enabled = true;

		capsule = gameObject.AddComponent<CapsuleCollider> ();
		capsule.radius = lineWidth / 2; // same width as the line renderer
		capsule.center = Vector3.zero; // center it to 0, 0, 0
		capsule.direction = 2; // z-axis for easier lookAt orientation... apparently it helps!

		isComplete = true;
	}
}
