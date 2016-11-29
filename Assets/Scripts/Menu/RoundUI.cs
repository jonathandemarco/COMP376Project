using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RoundUI : MonoBehaviour {

    public GameObject statsP1;
    public GameObject statsP2;
    public GameObject statsP3;
    public GameObject statsP4;

	public GameObject[] players;
	public Text firstPlaceText;

	public Text mapSelection;
	public float tTime;

	private int maxScore;

	// Set the player text in the canvas
	void Start () {
		print (GameState.accumScoreList.Count);

		firstPlaceText.rectTransform.position = new Vector3 (-340, 190, 0);

        Stats playerStats;
        /*If you're wondering why there's a try catch, there's a retarded bug 
         where statsPX is both equal to null and not null at the same time 
         (yes, at the same time ...). So don't remove the try catch, or everything breaks*/
        switch (GameState.playerCount)
        {
            case 4:
                try {
					Text killdeath = statsP4.transform.GetChild(0).GetComponent<Text>();
					Text score = statsP4.transform.GetChild(1).GetComponent<Text>();
                    playerStats = GameState.roundStats[3];

					killdeath.text = playerStats.kills + "-" + playerStats.deaths;
					score.text = "" + GameState.accumScoreList [3];

					if(GameState.accumScoreList[3] > maxScore){
						maxScore = GameState.accumScoreList[3];
					}

					statsP4.SetActive(true);
					players[3].SetActive(true);
                } catch (Exception e) { }
                goto case 3;
            case 3:
                try{
					Text killdeath = statsP3.transform.GetChild(0).GetComponent<Text>();
					Text score = statsP3.transform.GetChild(1).GetComponent<Text>();
                    playerStats = GameState.roundStats[2];

					killdeath.text = playerStats.kills + "-" + playerStats.deaths;
					score.text = "" + GameState.accumScoreList [2];

					if(GameState.accumScoreList[2] > maxScore){
						maxScore = GameState.accumScoreList[2];
					}					

					statsP3.SetActive(true);
					players[2].SetActive(true);
                } catch (Exception e) { }
                goto case 2;
            case 2:
                try {
					Text killdeath = statsP2.transform.GetChild(0).GetComponent<Text>();
					Text score = statsP2.transform.GetChild(1).GetComponent<Text>();
                    playerStats = GameState.roundStats[1];

					killdeath.text = playerStats.kills + "-" + playerStats.deaths;
					score.text = "" + GameState.accumScoreList [1];

					if(GameState.accumScoreList[1] > maxScore){
						maxScore = GameState.accumScoreList[1];
					}				

					statsP2.SetActive(true);
					players[1].SetActive(true);
                } catch (Exception e){}
                goto case 1;
            case 1:
                try {
					Text killdeath = statsP1.transform.GetChild(0).GetComponent<Text>();
					Text score = statsP1.transform.GetChild(1).GetComponent<Text>();
                    playerStats = GameState.roundStats[0];

					killdeath.text = playerStats.kills + "-" + playerStats.deaths;
					score.text = "" + GameState.accumScoreList [0];

					if(GameState.accumScoreList[0] > maxScore){
						maxScore = GameState.accumScoreList[0];
					}					

					statsP1.SetActive(true);
					players[0].SetActive(true);
                } catch (Exception e){}
                break;
        }

		int count = 0;
		for(int i = 0; i < GameState.playerCount; i++){
			if(GameState.accumScoreList[i] == maxScore){
				// scale the player with highest score
				players [i].transform.localScale = players [i].transform.localScale + new Vector3(50.0f, 50.0f, 50.0f);

				firstPlaceText.rectTransform.position = new Vector3 (-340, 190, 0);

				// place leader text on the character
				firstPlaceText.text = "Leader";
				firstPlaceText.rectTransform.position += new Vector3(200 * (i + 1), 0, 0);
				++count;
			}

			if (count > 1) {
				// set the leader text on the side
				firstPlaceText.text = "Leaders";
				firstPlaceText.rectTransform.position = new Vector3 (-410, 150, 0);
			}
		}

    }

	public void Update(){
		if (mapSelection != null) {
			mapSelection.color = new Color (mapSelection.color.r, mapSelection.color.g, mapSelection.color.b, Mathf.PingPong (Time.time, 1));
			tTime += Time.deltaTime;
		}
	}

    public void loadScene(string name)
    {
        GameState.loadScene(name);
    }
		
}
