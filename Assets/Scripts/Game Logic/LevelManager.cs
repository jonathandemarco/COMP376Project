using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public GameObject playerPrefab;
	private List<GameObject> playersList = new List<GameObject> ();
	private float time; 

	// Use this for initialization
	void Start () {
		addPlayersToScene (GameState.playerCount);
	}
	
	// Update is called once per frame
	void Update () {
	
		time -= Time.deltaTime;

		int winningPlayer = isRoundOver ();
		if (winningPlayer != -1) {
			endRound (winningPlayer);
		}

	}

	int isRoundOver() {
		
		foreach(GameObject player in playersList) {
			switch (GameState.gameMode) {

			case GameMode.STOCK:
				return stockModeRoundOver ();
			case GameMode.TIMER:
				return timerModeRoundOver ();
			}
		}
		return -1;
	}

	int stockModeRoundOver () {
		int winningplayer = -1;

		for (int i = 0; i < playersList.Count; i++) {
			PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
			if(script.getNumLives() > 0) {
				//no one won
				if (winningplayer > -1) {
					return -1;
				}
				winningplayer = i;
			}
		}

		//index of player who won
		return winningplayer;
	}

	int timerModeRoundOver() {
		if (time > 0) {
			return -1;
		} 
		else {
			int winningplayer = -1;

			for(int i = 0; i < playersList.Count; i++) {
				PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
				if (winningplayer == -1) {
					winningplayer = script.getScore ();
				} 
				else if (script.getScore () > winningplayer) {
					winningplayer = i;
				}
			}
			return winningplayer;
		}
	}

	void addPlayersToScene(int playerCount) {
		
		for (int i = 0; i < playerCount; i++) {
			GameObject playerObj = Instantiate (playerPrefab);
			playerObj.transform.position = getInitialSpawn (i);
			playersList [i] = playerObj;
		}
	}

	private Vector3 getInitialSpawn(int playerIndex) {
		//TODO: WHERE ARE RESPAWN POINTS FOR EACH PLAYER (4 CORNERS?)

		return new Vector3 ();
	}

	public List<GameObject> getScenePlayers() {
		return playersList;
	}

	public Vector3 getRespawnPoint() {
		//RETURN RESPAWN POINT
		return new Vector3 ();
	}

	public float getTime() {
		return time;
	}

	private void endRound (int winningPlayer) {
		//TODO: change scenes, display the winner of the round
	}

	public void setTimeLeft(float time) {
		this.time = time;
	}
}
