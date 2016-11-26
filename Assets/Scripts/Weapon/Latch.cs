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
	private Renderer [] renderers;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		time = 0.0f;
		initialDistance = handle.transform.localPosition - transform.localPosition;
	}
	
	// Update is called once per frame
	public override void Update () {	
	
		if (isUsed) {
			Vector3 currentDistance = handle.transform.localPosition - transform.localPosition;
			float maxDistanceBetweenWep = (currentDistance - maxVectorDistance).magnitude;
			float minDistanceBetweenWep = (currentDistance - initialDistance).magnitude;

			if (isLaunched && maxDistanceBetweenWep < maxDistance) {
				transform.position = handle.transform.forward * time;
				time += Time.deltaTime;
			} else if (isLaunched && maxDistanceBetweenWep >= maxDistance) {
				// reached max distance
				time = 0.0f;
				time += Time.deltaTime;
				Pull ();
			} else if (!isLaunched && isUsed && minDistanceBetweenWep > 0) {
				transform.position = -handle.transform.forward * time;
				time += Time.deltaTime;
			} else if (!isLaunched && isUsed && minDistanceBetweenWep <= 0){
				for(int r = 0; r < renderers.Length; ++r){
					renderers [r].enabled = false;
				}
				time = 0.0f;
				isUsed = false;
			}
		}
	}

	public void Launch(Renderer [] rend){
		renderers = rend;
		isUsed = true;
		isLaunched = true;
	}

	public void Pull(){
		isLaunched = false;
	}
}
