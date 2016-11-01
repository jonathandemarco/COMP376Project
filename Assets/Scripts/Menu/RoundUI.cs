using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundUI : MonoBehaviour {

	public Text scoreText;

	// Set the player text in the canvas
	void Start () {
	
		scoreText.text = "Score: \n\n";

		print (GameState.accumScoreList.Count);
		for (int i = 0; i < GameState.playerCount; i++) {
			scoreText.text += "Player " + (i + 1) + ": " + GameState.accumScoreList [i] + "\n";
		}

	}
		
}
