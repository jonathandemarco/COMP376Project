using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour {
		
	public Transform cameraRot;
	public float speed;
	private float time;

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		transform.rotation = cameraRot.rotation * Quaternion.Euler (0, speed * time, 0);
	}
}
