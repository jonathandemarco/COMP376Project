using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum GameMode {STOCK, TIMER};
public class GameState : MonoBehaviour {

	public int winScore;

	List<int> accumScore = new List<int>();

	public static GameMode gameMode;

	private int playerCount;

	public static void resetSettings() {
		gameMode = null;		
	}

	public static int isGameOver() {

		for(int i = 0; i < accumScore.Count; i++) {
			if (accumScore [i] == winScore)
				return i;
		}

		return -1;
	}

	public static void setWinningPlayer(int index) {
		accumScore [i]++;
		int winningPlayer = isGameOver ();
		if(winningPlayer != -1) {
			gameOverScene (winningPlayer);
		}
	}

	private void gameOverScene(int winningPlayer) {
		//TODO: change to game over scene showing winning player
	}

	public void initializeSettings(GameMode gameMode, int playerCount, int winScore) {
		this.gameMode = gameMode;
		this.playerCount = playerCount;
		this.winScore = winScore;
	}
}
