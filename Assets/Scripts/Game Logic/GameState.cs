using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameMode {STOCK, TIMER};

public class GameState : MonoBehaviour{

	public static GameMode gameMode;
	public static LevelManager currentLevelManager;
	public static int playerCount = 4;
	public static bool camFollow = true;
	public static int winScore;
	public static float gameTime;
	public static List<int> accumScoreList = new List<int>();
    public static string level;

	//returns index of winning player or -1
	public static List<int> isGameOver() {

        List<int> winningPlayers = new List<int>();
        winningPlayers.Add(-1);

        for (int i = 0; i < accumScoreList.Count; i++) {
			if (accumScoreList [i] == winScore) {
				if (winningPlayers [0] == -1) {
					winningPlayers.Clear ();
				}
				winningPlayers.Add (i);
			}
		}

		return winningPlayers;
	}

	public static void setWinningPlayers(List<int> indexes) {

		for(int i = 0; i < indexes.Count; i++) {
			accumScoreList[indexes[i]]++;
		}
		List<int> winningPlayers = isGameOver ();
		if(winningPlayers[0] != -1) {
			gameOverScene (winningPlayers);
			return;
		}

        //display the winner/winners of the round
        roundOverScene(winningPlayers);
    }

	public static void initializeSettings(GameMode gameMode, int playerCount, int winScore, float gameTime, string level) {
		GameState.gameMode = gameMode;
		GameState.playerCount = playerCount;
		GameState.winScore = winScore;
		GameState.gameTime = gameTime;
        GameState.level = level;
		accumScoreList.Clear ();

        for (int i = 0; i < playerCount; i++) {
            accumScoreList.Add(0);
        }
    }
		
    private static void roundOverScene(List<int> winningPlayers){    
		
		SceneManager.LoadScene("RoundOver");

    }

    private static void gameOverScene(List<int> winningPlayers) {
       
		SceneManager.LoadScene("GameOver");
    }

	public void loadScene(string levelName) {
        //load level levelName
        SceneManager.LoadScene(levelName);
	}

	public void reloadScene(){
		SceneManager.LoadScene (level);
	}
}
