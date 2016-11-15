using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCenter : MonoBehaviour {
	new Camera camera;
	Vector3 previousPos;
	public float interpolationSpeed;
	// Use this for initialization
	void Start () {
		camera = transform.GetChild (0).GetComponent<Camera> ();
		previousPos = camera.gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.camFollow) {
			List<GameObject> players = GameState.currentLevelManager.getScenePlayers ();
			Vector3 min = new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
			Vector3 max = new Vector3 (Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
			for (int i = 0; i < players.Count; i++) {
				if (players [i].GetComponent<PlayerManager> ().getHealth () > 0) {
					Vector3 pMax = camera.WorldToScreenPoint (players [i].transform.FindChild ("Model").GetComponent<Renderer> ().bounds.max);
					Vector3 pMin = camera.WorldToScreenPoint (players [i].transform.FindChild ("Model").GetComponent<Renderer> ().bounds.min);
					if (pMax.x > max.x)
						max.x = pMax.x;
					if (pMax.y > max.y)
						max.y = pMax.y;
					if (pMax.z > max.z)
						max.z = pMax.z;

					if (pMin.x < min.x)
						min.x = pMin.x;
					if (pMin.y < min.y)
						min.y = pMin.y;
					if (pMin.z < min.z)
						min.z = pMin.z;
				}
			}
			
			Vector3 center = camera.ScreenToWorldPoint ((max + min) / 2);
			transform.localPosition += (center - transform.localPosition) * interpolationSpeed * Time.deltaTime;
			Vector3 diff = (camera.ScreenToWorldPoint (max) - camera.ScreenToWorldPoint (min)) / 2;

			float h = Mathf.Max (diff.x, Mathf.Sqrt (diff.y * diff.y + diff.z * diff.z));
			float f = h / Mathf.Tan (60 * Mathf.Deg2Rad);

			f = Mathf.Max (10.0f, f);

			camera.gameObject.transform.localPosition = camera.gameObject.transform.localPosition.normalized * f;
		}
//		else
			
	}
}
