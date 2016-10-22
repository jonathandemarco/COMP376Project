using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameMode {STOCK, TIMER};

public class GameState : MonoBehaviour {

	private static int winScore;

	private static List<int> accumScore = new List<int>();

	public static GameMode gameMode;

	private static int playerCount;

	public static void resetSettings() {
		gameMode = 0;		
	}

	public static int isGameOver() {

		for(int i = 0; i < accumScore.Count; i++) {
			if (accumScore [i] == winScore)
				return i;
		}

		return -1;
	}

	public static void setWinningPlayer(int index) {
		accumScore [index]++;
		int winningPlayer = isGameOver ();
		if(winningPlayer != -1) {
			gameOverScene (winningPlayer);
		}
	}

	private static void gameOverScene(int winningPlayer) {
		//TODO: change to game over scene showing winning player
	}

	public void initializeSettings(GameMode gameMode, int playerCount, int winScore) {
		GameState.gameMode = gameMode;
		GameState.playerCount = playerCount;
		GameState.winScore = winScore;
	}
}
