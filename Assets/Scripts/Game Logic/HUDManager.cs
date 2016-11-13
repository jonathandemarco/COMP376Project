using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	public static HUDManager currentHUD;
	public GameObject textPrefab;
	public GameObject healthBarPrefab;
	new public Camera camera;
	private float time;
	private TextMesh timeKeeper;
	public List<PlayerManager> players;
	// Use this for initialization
	void Start () {
		List<GameObject> p = GameState.currentLevelManager.getScenePlayers ();

		players = new List<PlayerManager>();

		for (int i = 0; i < p.Count; i++)
			players.Add(p[i].GetComponent<PlayerManager> ());
		
		currentHUD = GetComponent<HUDManager>();

		camera = GameObject.Find ("HUDCam").GetComponent<Camera>();
		float width = camera.pixelWidth;
		float height = camera.pixelHeight;

		float xMargin = 0;
		float yMargin = 0.05f * Screen.height;

		for (int i = 0; i < p.Count; i++) {
			GameObject hB = Instantiate (healthBarPrefab, transform) as GameObject;
			hB.transform.localScale = new Vector3 (hB.transform.localScale.x * width / 1000, hB.transform.localScale.y * height / 1000, hB.transform.localScale.z);
			Bounds bounds = hB.GetComponent<Renderer> ().bounds;
			Renderer[] components = hB.GetComponentsInChildren<Renderer> ();

			foreach (Renderer r in components) {
				bounds.Encapsulate (r.bounds);
			}

			Vector3 size = bounds.size;

			if(i == 0)
				hB.transform.position = new Vector3(size.x / 2, - size.y / 2, hB.transform.position.z) + camera.ScreenToWorldPoint(new Vector3(xMargin, Screen.height - yMargin, 0));
			else if(i == 1)
				hB.transform.position = new Vector3(- size.x, - size.y / 2, hB.transform.position.z) + camera.ScreenToWorldPoint(new Vector3(Screen.width - xMargin, Screen.height - yMargin, 0));
			else if(i == 2)
				hB.transform.position = new Vector3(size.x / 2, size.y / 2, hB.transform.position.z) + camera.ScreenToWorldPoint(new Vector3(xMargin, yMargin, 0));
			else if(i == 3)
				hB.transform.position = new Vector3(- size.x, size.y / 2, hB.transform.position.z) + camera.ScreenToWorldPoint(new Vector3(Screen.width - xMargin, yMargin, 0));

			hB.GetComponentInChildren<PlayerStatus> ().setAvatar(players[i]);
		}

		timeKeeper = (Instantiate (textPrefab, transform.position + new Vector3(0, 14, 0), Quaternion.identity, transform) as GameObject).GetComponent<TextMesh>();
		timeKeeper.transform.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
		timeKeeper.gameObject.layer = LayerMask.NameToLayer ("HUD");
	}
	
	// Update is called once per frame
	void Update () {
		time = getTime();
		timeKeeper.text = "" + (int)time / 60 + ":" + ((int)time % 60 < 10 ? "0" : "") + (int)time % 60;
	}

	public void intializeHUD(GameObject[] playerObjects) {
		for(int i = 0; i < playerObjects.Length; i++) {
			//TODO: instantiate prefabs player stock / healthbar in HUD
		}
	}

	public float getTime()
	{
		return GameState.currentLevelManager.getTime();
	}

	public void update(PlayerManager player)
	{
		for (int i = 0; i < players.Count; i++) {
			if (players != null && players[i] != null && players [i] == player) {
				transform.GetComponentsInChildren<StatusBar> () [i].setValue (player.getHealth (), player.maxHealth);
				transform.GetChild(i).GetComponentInChildren<PlayerStatus> ().setAvatar(players[i]);
				return;
			}
		}
	}
}
