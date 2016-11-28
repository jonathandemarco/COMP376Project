using UnityEngine;
using System.Collections;

public class SuctionPS : MonoBehaviour {
	float time;
	public float freq;
	ParticleSystem.EmitParams e;
	// Use this for initialization
	void Start () {
		time = 0;
		e = new ParticleSystem.EmitParams ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > (1.0f / freq)) {
			float theta = Random.Range (0.0f, Mathf.PI * 2.0f);
			Vector3 pos = 10.0f * Mathf.Cos (theta) * transform.right + 10.0f * Mathf.Sin (theta) * transform.up + transform.forward * 40.0f;
			e.position = pos;
			e.velocity = (transform.position - e.position).normalized * 20.0f;
			GetComponent<ParticleSystem> ().Emit (e, 10);
			time = 0;
		}
	}
}
