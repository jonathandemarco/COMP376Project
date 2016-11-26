using UnityEngine;
using System.Collections;

public class IronLine : Weapon {
	private LineRenderer lineRenderer;

	public GameObject origin;
	public GameObject destination;
	public float lineWidth;

	private CapsuleCollider capsule;
	private bool isComplete;

	void Start(){
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
	}

	// Use this for initialization
	public override void Update () {
		if (destination.GetComponent<Latch> ().GetUse ()) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, origin.transform.position);
			lineRenderer.SetPosition (1, destination.transform.position);
			lineRenderer.SetWidth (lineWidth, lineWidth);

		} else {
			lineRenderer.enabled = false;
		}
	}
}
