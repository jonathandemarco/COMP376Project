using UnityEngine;
using System.Collections;

public class loadfx : MonoBehaviour {

	Light childlight;
//	AudioSource loadsound;
//	AudioClip load;

	// Use this for initialization
	void Start () {
//		loadsound = GetComponent<AudioSource>();
//		load = loadsound.clip;
//		AudioSource.PlayClipAtPoint(load, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.GetChild(0).transform.localScale += new Vector3 (0.33f, 0.33f, 0.33f) * Time.deltaTime;


	}
}
