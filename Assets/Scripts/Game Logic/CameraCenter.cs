using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCenter : MonoBehaviour {
	Camera camera;
	Vector3 trueCenter;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		camera = transform.GetChild (0).GetComponent<Camera> ();
		offset = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		bool zoomOut = false;
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

				if (p.x - 50 < 0 || p.y - 50 < 0 || p.x + 50 > Screen.width || p.y + 50 > Screen.height)
					zoomOut = true;
			}
		}

		if(zoomOut)
			camera.gameObject.transform.localPosition += camera.gameObject.transform.localPosition.normalized * 0.02f;
		else
			camera.gameObject.transform.localPosition -= camera.gameObject.transform.localPosition.normalized * 0.02f;
		
		Vector3 center = camera.ScreenToWorldPoint((max + min) / 2);
		transform.position = new Vector3(center.x, 0, center.z);
	}
}
