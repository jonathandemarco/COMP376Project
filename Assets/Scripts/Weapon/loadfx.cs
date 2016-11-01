using UnityEngine;
using System.Collections;

public class loadfx : MonoBehaviour {

	Light childlight;


	void Update () {
		
		transform.GetChild(0).transform.localScale += new Vector3 (0.33f, 0.33f, 0.33f) * Time.deltaTime;


	}
}
