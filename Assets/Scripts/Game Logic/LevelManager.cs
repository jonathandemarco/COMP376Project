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

		List<int> winningPlayers = isRoundOver ();
		if (winningPlayers[0] != -1) {
			endRound (winningPlayers);
		}

	}

	//returns list of winners or -1 at index 0
	List<int> isRoundOver() {
		
		foreach(GameObject player in playersList) {
			switch (GameState.gameMode) {

			case GameMode.STOCK:
				return stockModeRoundOver ();
			case GameMode.TIMER:
				return timerModeRoundOver ();
			}
		}
		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;
		return winningplayers;
	}

	List<int> stockModeRoundOver () {
		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;

		for (int i = 0; i < playersList.Count; i++) {
			PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
			if(script.getNumLives() > 0) {
				//no one won
				if (winningplayers[0] > -1) {
					winningplayers.Clear;
					winningplayers [0] = -1;
					return winningplayers;
				}
				winningplayers[0] = i;
			}
		}

		//index of player who won
		return winningplayers;
	}

	List<int> timerModeRoundOver() {
		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;

		if (time > 0) {
			return winningplayers;
		} 
		else {
			int count = 0;
			int topScore = -1;
			for(int i = 0; i < playersList.Count; i++) {
				PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
				if (topScore == -1) {
					topScore = script.getScore ();
				} 
				else if (script.getScore () > topScore) {
					topScore = script.getScore ();
				}
			}

			for (int i = 0; i < playersList.Count; i++) {
				PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
				if (script.getScore () == topScore) {
					winningplayers [count++] = i;
				}
			}
		
			return winningplayers;
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
