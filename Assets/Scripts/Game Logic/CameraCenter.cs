using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCenter : MonoBehaviour {
	Camera camera;
	Vector3 previousPos;
	// Use this for initialization
	void Start () {
		camera = transform.GetChild (0).GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool zoomOut = false;
		bool change = false;
		List<GameObject> players = GameState.currentLevelManager.getScenePlayers ();
		Vector3 min = new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		Vector3 max = new Vector3 (Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
		for (int i = 0; i < players.Count; i++) {
			if (players [i].GetComponent<PlayerManager> ().getHealth () > 0) {
				Vector3 p = camera.WorldToScreenPoint (players [i].transform.position);
				if (p.x > max.x)
					max.x = p.x;
				if (p.y > max.y)
					max.y = p.y;
				if (p.z > max.z)
					max.z = p.z;

				if (p.x < min.x)
					min.x = p.x;
				if (p.y < min.y)
					min.y = p.y;
				if (p.z < min.z)
					min.z = p.z;

				if (p.x - 80 < 0 || p.y - 80 < 0 || p.x + 80 > Screen.width || p.y + 80 > Screen.height)
					zoomOut = true;
			}
		}

		Vector3 center = camera.ScreenToWorldPoint((max + min) / 2);
		transform.position = new Vector3(center.x, 0, center.z);
		previousPos = transform.position;

		if (zoomOut)
			camera.gameObject.transform.localPosition += camera.gameObject.transform.localPosition.normalized * 0.04f;
		else
			camera.gameObject.transform.localPosition -= camera.gameObject.transform.localPosition.normalized * 0.04f;
	}
}
