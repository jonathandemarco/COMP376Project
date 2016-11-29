using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOverController : MonoBehaviour {

	public GameObject statsP1;
	public GameObject statsP2;
	public GameObject statsP3;
	public GameObject statsP4;

	public GameObject[] players;

    public Text winnerText;
    AudioSource swordSound;
    string levelName;

	// Use this for initialization
	void Start () {
        winnerText.text = "Player " + GameState.winner + " Wins!";
        swordSound = GetComponent<AudioSource>();

		Stats playerStats;

		// show the winning player
		players [GameState.winner - 1].SetActive (true);

		switch (GameState.playerCount)
		{
		case 4:
			try {
				Text killdeath = statsP4.transform.GetChild(0).GetComponent<Text>();
				Text score = statsP4.transform.GetChild(1).GetComponent<Text>();
				playerStats = GameState.roundStats[3];

				killdeath.text = playerStats.kills + "-" + playerStats.deaths;
				score.text = "" + GameState.accumScoreList [3];

				statsP4.SetActive(true);
			} catch (Exception e) { }
			goto case 3;
		case 3:
			try{
				Text killdeath = statsP3.transform.GetChild(0).GetComponent<Text>();
				Text score = statsP3.transform.GetChild(1).GetComponent<Text>();
				playerStats = GameState.roundStats[2];

				killdeath.text = playerStats.kills + "-" + playerStats.deaths;
				score.text = "" + GameState.accumScoreList [2];

				statsP3.SetActive(true);
			} catch (Exception e) { }
			goto case 2;
		case 2:
			try {
				Text killdeath = statsP2.transform.GetChild(0).GetComponent<Text>();
				Text score = statsP2.transform.GetChild(1).GetComponent<Text>();
				playerStats = GameState.roundStats[1];

				killdeath.text = playerStats.kills + "-" + playerStats.deaths;
				score.text = "" + GameState.accumScoreList [1];

				statsP2.SetActive(true);
			} catch (Exception e){}
			goto case 1;
		case 1:
			try {
				Text killdeath = statsP1.transform.GetChild(0).GetComponent<Text>();
				Text score = statsP1.transform.GetChild(1).GetComponent<Text>();
				playerStats = GameState.roundStats[0];

				killdeath.text = playerStats.kills + "-" + playerStats.deaths;
				score.text = "" + GameState.accumScoreList [0];

				statsP1.SetActive(true);
			} catch (Exception e){}
			break;
		}
    }

    public void loadScene(string name)
    {
        swordSound.Play();
        Invoke("LoadLevelAfterSoundEnd", 1.0f);
        levelName = name;
    }

    public void LoadLevelAfterSoundEnd()
    {
        GameState.loadScene(levelName);
    }
}
