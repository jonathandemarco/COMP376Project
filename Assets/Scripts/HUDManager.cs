using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	float timeLeft;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.gameMode == GameMode.TIMER) {
			timeLeft -= Time.deltaTime;
			//TODO: updtae time on HUD
		}
	}

	public void setTimeLeft(float t) {
		timeLeft = t;
	}

	public void intializeHUD(GameObject[] playerObjects) {

		for(int i = 0; i < playerObjects.Length; i++) {
			//TODO: instantiate prefabs player stock / healthbar in HUD
		}
	}

}
