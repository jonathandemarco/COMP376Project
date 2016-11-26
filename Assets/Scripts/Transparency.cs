using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 0.5f);
	}
	

}
