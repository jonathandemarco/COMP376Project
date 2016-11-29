using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RoundUI : MonoBehaviour {

	public Text scoreText;
    public GameObject statsP1;
    public GameObject statsP2;
    public GameObject statsP3;
    public GameObject statsP4;

	// Set the player text in the canvas
	void Start () {
	
		scoreText.text = "Kills/Deaths: \n\nScore: \n\n";

		print (GameState.accumScoreList.Count);
		for (int i = 0; i < GameState.playerCount; i++) {
            scoreText.text += "Player " + (i + 1) + ": " + GameState.accumScoreList [i] + "\n";           
        }

        Stats playerStats;
        /*If you're wondering why there's a try catch, there's a retarded bug 
         where statsPX is both equal to null and not null at the same time 
         (yes, at the same time ...). So don't remove the try catch, or everything breaks*/
        switch (GameState.playerCount)
        {
            case 4:
                try {
                    statsP4.SetActive(true);
                    Text textStats = statsP4.transform.GetChild(0).GetComponent<Text>();
                    playerStats = GameState.roundStats[3];
                    textStats.text = playerStats.kills + "-" + playerStats.deaths;
                } catch (Exception e) { }
                goto case 3;
            case 3:
                try{
                    statsP3.SetActive(true);
                    Text textStats = statsP3.transform.GetChild(0).GetComponent<Text>();
                    playerStats = GameState.roundStats[2];
                    textStats.text = playerStats.kills + "-" + playerStats.deaths;
                } catch (Exception e) { }
                goto case 2;
            case 2:
                try {
                    statsP2.SetActive(true);
                    Text textStats = statsP2.transform.GetChild(0).GetComponent<Text>();
                    playerStats = GameState.roundStats[1];
                    textStats.text = playerStats.kills + "-" + playerStats.deaths;
                } catch (Exception e){}
                goto case 1;
            case 1:
                try {
                    statsP1.SetActive(true);
                    Text textStats = statsP1.transform.GetChild(0).GetComponent<Text>();
                    playerStats = GameState.roundStats[0];
                    textStats.text = playerStats.kills + "-" + playerStats.deaths;
                } catch (Exception e){}
                break;
        }

    }

    public void loadScene(string name)
    {
        GameState.loadScene(name);
    }
		
}
