using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	//UI Sounds
	AudioSource[] uiSounds;
	AudioSource swordSound2;
	AudioSource startSound;
	AudioSource swordStart;

	//Input pressed by players
	public bool player1Start;
	public bool player2Start;
	public bool player3Start;
	public bool player4Start;

	//Platforms
	public GameObject platforms;

	//Icons representing player that will play
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	//Buttons appearing after Start has been pressed
	public GameObject p2Button;

	//choosing map buttons
	public GameObject map1Button;
	public GameObject map2Button;

	public Text title;

	//Start Text
	public Text pressStartText;
	private bool hasPressedStart = false;
	private bool isBlinking = false;
	//levelshortcut because you can't pass parameters to 'Invoke'
	private string lvl;

	[SerializeField] int numOfPlayers = 0;

	void Start(){
		InvokeRepeating("BlinkText", 0.0f, 0.6f);
		uiSounds = GetComponents<AudioSource>();
		swordStart = uiSounds[0];
		startSound = uiSounds[1];
		swordSound2 = uiSounds[2];
	}

	void Update(){

		if(!hasPressedStart && Input.GetKeyDown("space")){
			hasPressedStart = true;
			pressStartText.enabled = false;
			CancelInvoke ();

			platforms.SetActive (true);

			title.transform.position += new Vector3 (0, 150, 0);
			title.text = "Join the battle dreamers!";
			title.color = Color.white;

			swordStart.Play();
		}

		if (numOfPlayers > 1 && !p2Button.activeSelf && !map1Button.activeSelf) {
			p2Button.SetActive(true);
		}

		AddPlayers ();
	}

	public void LoadLevel(string level){
		startSound.Play ();
		GameState.initializeSettings (GameMode.STOCK, GameState.playerCount, 1, 300.0f, level);
		Invoke ("LoadLevelAfterSoundEnd", 2.0f);
	}

	public void LoadLevelAfterSoundEnd () {
		SceneManager.LoadScene ("xbox");
	}

	public void SetNumOfPlayers(){
		swordSound2.Play();
		GameState.playerCount = numOfPlayers;

		p2Button.SetActive (false);
		platforms.SetActive (false);

		title.text = "Choose the area!";

		map1Button.SetActive (true);
		map2Button.SetActive (true);
	}

	void AddPlayers(){
		if (Input.GetKeyDown("space")) {
			if (!player1Start) {

				Debug.Log ("Player 1");

				// Add gameobject to show player popping out of screen
				player1.SetActive(true);

				numOfPlayers++;
				player1Start = true;

				swordStart.Play();
			}
		}
		if (Input.GetButtonDown ("A0")) {
			if (!player2Start) {

				// Add gameobject to show player popping out of screen
				player2.SetActive(true);

				numOfPlayers++;
				player2Start = true;

				swordStart.Play();
			}
		}
		if (Input.GetButtonDown ("B0")) {
			if (!player3Start) {

				// Add gameobject to show player popping out of screen
				player3.SetActive(true);

				numOfPlayers++;
				player3Start = true;
			}
		}
		if (Input.GetButtonDown ("C0")) {
			if (!player4Start) {

				// Add gameobject to show player popping out of screen
				player4.SetActive(true);

				numOfPlayers++;
				player4Start = true;
			}
		}

	}

	void ShowCurrentPlayers(){

	}

	void BlinkText(){

		if (!isBlinking) {
			pressStartText.enabled = true;
			isBlinking = true;
		} else {
			pressStartText.enabled = false;
			isBlinking = false;
		}
	}


}
