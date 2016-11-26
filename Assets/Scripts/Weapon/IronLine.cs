using UnityEngine;
using System.Collections;

public class IronLine : Weapon {
	private LineRenderer lineRenderer;

	public GameObject origin;
	public GameObject destination;
	public float lineWidth;
	public GameObject SphericalMachine;

	private CapsuleCollider capsule;
	private bool isComplete;

	void Start(){
		lineRenderer = GetComponent<LineRenderer> ();
		capsule = gameObject.AddComponent<CapsuleCollider> ();

		capsule.radius = lineWidth / 2; // same width as the line renderer
		capsule.center = Vector3.zero; // center it to 0, 0, 0
		capsule.direction = 2; // z-axis for easier lookAt orientation... apparently it helps!
	}

	// Use this for initialization
	public override void Update () {
		lineRenderer.SetPosition (0, origin.transform.localPosition);
		lineRenderer.SetPosition (1, destination.transform.localPosition);
		lineRenderer.SetWidth (lineWidth, lineWidth);

		capsule.transform.position = origin.transform.localPosition + (destination.transform.localPosition - origin.transform.localPosition) / 2; // find the mid point
		capsule.transform.LookAt (origin.transform.localPosition);
		capsule.height = (destination.transform.localPosition - origin.transform.localPosition - Vector3.Normalize(destination.transform.localPosition - origin.transform.localPosition) * (SphericalMachine.GetComponent<SphereCollider>().radius * 2 + 0.1f)).magnitude;
	}
}
