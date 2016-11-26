using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	private bool isUsed;
	private bool isLaunched;
	private float time;
	private Vector3 initialDistance;
	private Vector3 maxVectorDistance;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		time = 0.0f;
		initialDistance = handle.transform.localPosition - transform.localPosition;
		maxVectorDistance = new Vector3 (0.0f, 0.0f, maxDistance);
	}
	
	// Update is called once per frame
	public override void Update () {	
	
		Vector3 currentDistance = handle.transform.localPosition - transform.localPosition;

		if (isLaunched && maxVectorDistance != currentDistance) {
			transform.position = handle.transform.right * time;
			time += Time.deltaTime;
		} else if (!isLaunched && isUsed && currentDistance != initialDistance) {
			transform.position = -handle.transform.right * time;
			time += Time.deltaTime;
		} else {
			time = 0.0f;
			isUsed = false;
		}
	}

	public void Launch(){
		isUsed = true;
		isLaunched = true;
	}

	public void Pull(){
		isLaunched = false;
	}
}
