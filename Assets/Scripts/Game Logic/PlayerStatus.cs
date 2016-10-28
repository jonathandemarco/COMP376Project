using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setAvatar(PlayerManager player)
	{
		GetComponent<MeshFilter> ().mesh = player.GetComponent<MeshFilter> ().mesh;
		GetComponent<Renderer> ().material = player.GetComponent<Renderer> ().material;
	}
}
