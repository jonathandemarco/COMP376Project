using UnityEngine;
using System.Collections.Generic;

public class Stats
{
    public int kills = 0;
    public int deaths = 0;

    public Stats()
    {
        kills = 0;
        deaths = 0;
    }
}

public class LevelManager : MonoBehaviour {
	public float itemDropProb;
	public GameObject cratePrefab;
	public GameObject playerPrefab;
	public GameObject HUDPrefab;
	public GameObject WeaponDatabase;
	private List<GameObject> playersList = new List<GameObject> ();
	private float timeLeft = 300.0f;

    //TODO: populate these from specific level manager
    public List<Vector3> initialSpawnsList = new List<Vector3>(); //initial player spawns
    public List<Vector3> allSpawnsList = new List<Vector3>(); //available spawn points in level
	public Material skyboxMat;
	public float skyBoxBlendSpeed;

	private bool skyboxIsIncrement = true;

    // Use this for initialization
    virtual public void Start () {
		if (GameState.gameMode == GameMode.STOCK)
			timeLeft = 0.0f;
		else if(GameState.gameMode == GameMode.TIMER)
			timeLeft = GameState.gameTime;
		
		setUpSpawnPoints ();
		GameState.currentLevelManager = GetComponent<LevelManager>();
		addPlayersToScene (GameState.playerCount);
		Instantiate (WeaponDatabase);
        Instantiate(HUDPrefab);
        RenderSettings.skybox = skyboxMat;
    }

    // Update is called once per frame
    virtual public void Update () {
		if (GameState.gameMode == GameMode.STOCK)
			timeLeft += Time.deltaTime;
		else if(GameState.gameMode == GameMode.TIMER)
			timeLeft -= Time.deltaTime;

		List<int> winningPlayers = isRoundOver ();
		if (winningPlayers[0] != -1) {
			endRound (winningPlayers);
		}

		if (1 - Random.Range (0.0f, 1.0f) < itemDropProb)
			spawnCrate ();

		updateSkybox ();
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

		List<int> winningplayers = new List<int>();
		winningplayers.Add(-1);

//		List<int> winningplayers = new List<int>(GameState.playerCount);
 //       winningplayers.Add(-1);


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
					topScore = GameState.roundStats[i].kills;
				} 
				else if (GameState.roundStats[i].kills > topScore) {
					topScore = GameState.roundStats[i].kills;
				}
			}

            winningplayers.Clear();

            for (int i = 0; i < playersList.Count; i++) {
				PlayerManager script = (PlayerManager) playersList [i].GetComponent (typeof(PlayerManager));
				if (GameState.roundStats[i].kills == topScore) {
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
			if(i == 0)
				playerObj.GetComponent<PlayerManager> ().setPlayerChar ('K');
			else
				playerObj.GetComponent<PlayerManager> ().setPlayerChar ((char)(64 + i));
			
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
        int playerCount = 0;

        for (int i = 0; i < allSpawnsList.Count; i++) {
            double avgDistance = 0;
            playerCount = 0;
            for (int j = 0; j < playersList.Count; j++) {
                if (j != playerIndex) {
                    if (((PlayerManager)playersList[j].GetComponent(typeof(PlayerManager))).getAlive())
                    {
                        avgDistance += Mathf.Abs(Vector3.Distance(allSpawnsList[i], playersList[j].transform.position));
                        playerCount++;
                    }
                }
            }

            avgDistance /= playersList.Count;

            if (avgDistance > maxAvgDistance) {
                maxAvgDistance = avgDistance;
                respawnIndex = i;
            }
        }

        if (playerCount == 0)
        {
            return allSpawnsList[(Random.Range(0, allSpawnsList.Count))];
        }
        return allSpawnsList[respawnIndex];
    }

    public List<GameObject> getScenePlayers() {
		return playersList;
	}

	public float getTime() {
		return timeLeft;
	}

	public void setTimeLeft(float time) {
		this.timeLeft = time;
	}

	public void incrementSkyboxBlend(float blendFactor) {
		
		blendFactor = skyboxIsIncrement ? blendFactor : blendFactor * -1;
		float newBlend = RenderSettings.skybox.GetFloat ("_Blend") + blendFactor;

		if (newBlend > 1.0f) {
			newBlend = 1.0f;
			skyboxIsIncrement = false;
		} else if (newBlend < 0.0f) {
			newBlend = 0.0f;
			skyboxIsIncrement = true;
		}

		RenderSettings.skybox.SetFloat("_Blend", newBlend);
	}

	public void setUpSpawnPoints()
	{
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).name == "SpawnPoint") {
				initialSpawnsList.Add (transform.GetChild (i).transform.position);
				allSpawnsList.Add (transform.GetChild (i).transform.position);
			}
		}
	}
		
	void updateSkybox () {
		incrementSkyboxBlend (skyBoxBlendSpeed);
	}

	void spawnCrate()
	{
		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		Instantiate (cratePrefab, new Vector3 (Random.Range(min.x + size.x * 0.1f, max.x - size.x * 0.1f), 10, Random.Range(min.z + size.z * 0.1f, max.z - size.z * 0.1f)), Quaternion.identity);
	}

    public void increaseKill(char playerChar)
    {
        int playerIndex = -1;
        switch (playerChar)
        {
            case 'K':
                playerIndex = 0;
                break;
            case 'A':
                playerIndex = 1;
                break;
            case 'B':
                playerIndex = 2;
                break;
            case 'C':
                playerIndex = 3;
                break;

        }
        if (playerIndex >= 0)
        {
            GameState.roundStats[playerIndex].kills++;
        }
    }

    public void increaseDeath(char playerChar)
    {
        int playerIndex = -1;
        switch(playerChar)
        {
            case 'K':
                playerIndex = 0;
                break;
            case 'A':
                playerIndex = 1;
                break;
            case 'B':
                playerIndex = 2;
                break;
            case 'C':
                playerIndex = 3;
                break;

        }
        if (playerIndex >= 0)
        {
            GameState.roundStats[playerIndex].deaths++;
        }
        logKillsDeaths();
    }

    public void logKillsDeaths()
    {
        for(int i = 0; i < playersList.Count; i++)
        {
            Debug.Log("Player " + i + ": " + GameState.roundStats[i].kills + "-" + GameState.roundStats[i].deaths);
        }
    }
}
