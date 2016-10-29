using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	public static HUDManager currentHUD;
	public GameObject healthBarPrefab;
	private float timeLeft;
	public List<PlayerManager> players;
	// Use this for initialization
	void Start () {
		List<GameObject> p = GameState.currentLevelManager.getScenePlayers ();

		players = new List<PlayerManager>();

		for (int i = 0; i < p.Count; i++)
			players.Add(p[i].GetComponent<PlayerManager> ());
		
		currentHUD = GetComponent<HUDManager>();
		for (int i = 0; i < p.Count; i++) {
			GameObject hB = Instantiate (healthBarPrefab, transform) as GameObject;
			hB.transform.position += new Vector3 (((i % 2) * 2 - 1) * 20 + 3, ((i / 2) * 2 - 1) * 10, 0);
			hB.GetComponentInChildren<PlayerStatus> ().setAvatar(players[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {

//		for (int i = 0; i < playerReferences.Count; i++)
//		{
//			PlayerManager player = playerReferences [i].GetComponent<PlayerManager> () as PlayerManager;
//			if(player.getNumLives() > 0)
//				transform.GetChild(i).GetComponent<StatusBar>().setValue(player.getHealth(), player.getMaxHealth());
//		}
		
	}

	public void intializeHUD(GameObject[] playerObjects) {

		for(int i = 0; i < playerObjects.Length; i++) {
			//TODO: instantiate prefabs player stock / healthbar in HUD
		}
	}

	public float getTime()
	{
		return 0;
	}

	public void update(PlayerManager player)
	{
		for (int i = 0; i < players.Count; i++) {
			if (players != null && players [i] == player) {
				transform.GetComponentsInChildren<StatusBar> () [i].setValue (player.getHealth (), player.maxHealth);
				transform.GetChild(i).GetComponentInChildren<PlayerStatus> ().setAvatar(players[i]);
				return;
			}
		}
	}
}
