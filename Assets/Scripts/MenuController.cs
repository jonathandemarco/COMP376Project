using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public bool player1Start;
	public bool player2Start;
	public bool player3Start;
	public bool player4Start;

	public GameObject player1_image;
	public GameObject player2_image;
	public GameObject player3_image;
	public GameObject player4_image;


	[SerializeField] int numOfPlayers = 0;

	void Update(){
		AddPlayers ();
	}

	public void LoadLevel(int num){
		GameState.playerCount = num;
		SceneManager.LoadScene ("Gladiators");
	}

	void AddPlayers(){
		if (Input.GetButtonDown ("A0")) {
			if (!player1Start) {
				numOfPlayers++;
				player1Start = true;
			}
		}
		if (Input.GetButtonDown ("A1")) {
			if (!player2Start) {
				numOfPlayers++;
				player2Start = true;
			}
		}
		if (Input.GetButtonDown ("A2")) {
			if (!player3Start) {
				numOfPlayers++;
				player3Start = true;
			}
		}
		if (Input.GetButtonDown ("A3")) {
			if (!player4Start) {
				numOfPlayers++;
				player4Start = true;
			}
		}

	}

	void ShowCurrentPlayers(){

	}


}
