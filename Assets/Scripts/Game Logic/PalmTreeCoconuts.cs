using UnityEngine;
using System.Collections;

public class PalmTreeCoconuts : MonoBehaviour {
	public GameObject coconutPrefab;
	public float period;
	public float maxOffset;
	float currentTime;
	// Use this for initialization
	void Start () {
		currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime > period + maxOffset) {
			Instantiate (coconutPrefab, transform.position - new Vector3(0, 0.001f, 0), Quaternion.identity);
			currentTime = 0;
			maxOffset = Random.Range(-maxOffset, maxOffset);
		}
		currentTime += Time.deltaTime;
	}
}
