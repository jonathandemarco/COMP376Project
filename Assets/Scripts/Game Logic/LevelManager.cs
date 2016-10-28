using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public GameObject playerPrefab;
	private List<GameObject> playersList = new List<GameObject> ();
	private float timeLeft = 300.0f;

    //TODO: populate these from specific level manager
    public List<Vector3> initialSpawnsList = new List<Vector3>(); //initial player spawns
    public List<Vector3> allSpawnsList = new List<Vector3>(); //available spawn points in level

    // Use this for initialization
    virtual public void Start () {
		GameState.currentLevelManager = GetComponent<LevelManager>();
		//addPlayersToScene (GameState.playerCount);
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

		List<int> winningplayers = new List<int>(GameState.playerCount);
		winningplayers.Add(-1);
		return winningplayers;
	}

	private List<int> stockModeRoundOver () {
		List<int> winningplayers = new List<int>(GameState.playerCount);
        winningplayers.Add(-1);

        int count = 0;
        int winner = -1;
        for (int i = 0; i < playersList.Count; i++) {
			PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
			if(script.getNumLives() > 0) {
                count++;
                if (count > 1)
                {
                    return winningplayers;
                }
                winner = i;
            }
		}

        //only 1 player with lives left
        if (count == 1)
        {
            winningplayers.Clear();
            winningplayers.Add(winner);
        }

		//player who won
		return winningplayers;
	}

	private List<int> timerModeRoundOver() {
		List<int> winningplayers = new List<int>();
        winningplayers.Add(-1);

        //is there time left?
		if (timeLeft > 0) {
			return winningplayers;
		} 
		else {
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

            winningplayers.Clear();

            for (int i = 0; i < playersList.Count; i++) {
				PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
				if (script.getScore () == topScore) {
					winningplayers.Add(i);
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

    private void endRound(List<int> winningPlayers)
    {
        GameState.setWinningPlayers(winningPlayers);
    }

    //called by player gameobject (in PlayerManager) when he needs his initial respawn point
    //ex: transform.position = GameState.currentLevelManager.getInitialSpawn()
    public Vector3 getInitialSpawn(int playerIndex) {
		return initialSpawnsList[playerIndex];
	}

    //called by player gameobject (in PlayerManager) when he needs a respawn point
    //ex: transform.position = GameState.currentLevelManager.getRespawnPoint()
    //choses the respawn point based on the largest avg distance
    public Vector3 getRespawnPoint(int playerIndex) {
        double maxAvgDistance = -1;
        int respawnIndex = -1;

        for (int i = 0; i < allSpawnsList.Count; i++) {
            double avgDistance = 0;
            for (int j = 0; j < playersList.Count; j++) {
                if (j != playerIndex) {
                    avgDistance += Vector3.Distance(allSpawnsList[i], playersList[j].transform.position);
                }
            }

            avgDistance /= 3;

            if (avgDistance > maxAvgDistance) {
                maxAvgDistance = avgDistance;
                respawnIndex = i;
            }
        }
        return allSpawnsList[respawnIndex];
    }

    public List<GameObject> getScenePlayers() {
		Debug.Log (playersList);
		return playersList;
	}

	public float getTime() {
		return timeLeft;
	}

	public void setTimeLeft(float time) {
		this.timeLeft = time;
	}

}
