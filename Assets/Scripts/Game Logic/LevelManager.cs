using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public GameObject playerPrefab;
	private List<GameObject> playersList = new List<GameObject> ();
	private float timeLeft = 300.0f; 

	// Use this for initialization
	virtual public void Start () {
		GameState.currentLevelManager = GetComponent<LevelManager>();
		addPlayersToScene (GameState.playerCount);
	}
	
	// Update is called once per frame
	virtual public void Update () {
	
		timeLeft -= Time.deltaTime;

		List<int> winningPlayers = isRoundOver ();
		if (winningPlayers[0] != -1) {
			endRound (winningPlayers);
		}

	}

	
	//returns list of winners or -1 at index 0 if theres no winner
	private List<int> isRoundOver() {
		
		switch (GameState.gameMode) {
			case GameMode.STOCK:
				return stockModeRoundOver ();
			case GameMode.TIMER:
				return timerModeRoundOver ();
		}

		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;
		return winningplayers;
	}

	private List<int> stockModeRoundOver () {
		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;

		for (int i = 0; i < playersList.Count; i++) {
			PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
			if(script.getNumLives() > 0) {
				//no one won
				if (winningplayers[0] > -1) {
					winningplayers.Clear();
					winningplayers [0] = -1;
					return winningplayers;
				}
				winningplayers[0] = i;
			}
		}

		//index of player who won
		return winningplayers;
	}

	private List<int> timerModeRoundOver() {
		List<int> winningplayers = new List<int>();
		winningplayers [0] = -1;

		if (timeLeft > 0) {
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

	private void addPlayersToScene(int playerCount) {
		for (int i = 0; i < playerCount; i++) {
			GameObject playerObj = Instantiate (playerPrefab);
			playerObj.transform.position = getInitialSpawn (i);
			playersList.Add(playerObj);
		}
	}

	private Vector3 getInitialSpawn(int playerIndex) {
		//TODO: WHERE ARE RESPAWN POINTS FOR EACH PLAYER (4 CORNERS?)

		return new Vector3 ();
	}

    private void endRound(List<int> winningPlayers)
    {
        //TODO: change scenes, display the winner/winners of the round
    }

    public List<GameObject> getScenePlayers() {
		Debug.Log (playersList);
		return playersList;
	}

    //called by player gameobject (in PlayerManager) when he needs a respawn point
    //ex: transform.position = GameState.currentLevelManager.getRespawnPoint()
    public Vector3 getRespawnPoint() {
		//return respawn point randomized and based on location of other players
		return new Vector3 ();
	}

	public float getTime() {
		return timeLeft;
	}

	public void setTimeLeft(float time) {
		this.timeLeft = time;
	}

}
