using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameMode {STOCK, TIMER};

public class GameState : MonoBehaviour {

	public static GameMode gameMode;
	public static LevelManager currentLevelManager;
	public static int playerCount;

	private static int winScore;
	private static List<int> accumScoreList = new List<int>();

	//returns index of winning player or -1
	public static List<int> isGameOver() {

        int count = 0;
        List<int> winningPlayers = new List<int>();
        winningPlayers[0] = -1;

        for (int i = 0; i < accumScoreList.Count; i++) {
            if (accumScoreList[i] == winScore)
                winningPlayers[count++] = i;
		}

		return winningPlayers;
	}

	public static void setWinningPlayer(int index) {
        accumScoreList[index]++;
		List<int> winningPlayers = isGameOver ();
		if(winningPlayers[0] != -1) {
			gameOverScene (winningPlayers);
		}
	}
		
	public static void initializeSettings(GameMode gameMode, int playerCount, int winScore) {
		GameState.gameMode = gameMode;
		GameState.playerCount = playerCount;
		GameState.winScore = winScore;
	}

	private static void gameOverScene(List<int> winningPlayers) {
        //TODO: change to game over scene showing winning player
    }

	public static void loadScene(string levelName) {
        //load level levelName
        SceneManager.LoadScene(levelName);
	}

}
