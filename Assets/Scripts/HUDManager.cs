using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	public GameObject healthBarPrefab;
	private float timeLeft;
	// Use this for initialization
	void Start () {
		List<GameObject> playerReferences = GameState.currentLevelManager.getScenePlayers ();

		for (int i = 0; i < playerReferences.Count; i++) {
			GameObject healthBar = Instantiate (healthBarPrefab) as GameObject;
			healthBar.transform.SetParent (transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		List<GameObject> playerReferences = GameState.currentLevelManager.getScenePlayers ();

		for (int i = 0; i < playerReferences.Count; i++)
		{
			PlayerManager player = playerReferences [i].GetComponent<PlayerManager> () as PlayerManager;
//			if(player.getNumLives() > 0)
//				transform.GetChild(i).GetComponent<StatusBar>().setValue(player.getHealth(), player.getMaxHealth());
		}
		
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
}
