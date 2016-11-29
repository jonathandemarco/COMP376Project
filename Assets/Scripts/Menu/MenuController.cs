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
	public GameObject settingsButton;
	public GameObject loadLevelButton;

	//choosing map buttons
	public GameObject map1Button;
	public GameObject map2Button;

	//choosing setting buttons
	public GameObject settingGame;
	public GameObject stockOption;
	public GameObject timerOption;

	public Text gameMode;
	public Text roundNumber;
	public Text stockNumber;
	public Text timeNumber;

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

			title.transform.position += new Vector3 (0, 200, 0);
			title.text = "Join the sleepover!";
			title.color = Color.white;

			swordStart.Play();
		}

		if (numOfPlayers > 1 && !settingsButton.activeSelf && !map1Button.activeSelf && !loadLevelButton.activeSelf) {
			settingsButton.SetActive(true);
		}

		AddPlayers ();
	}

	public void LoadLevel(string level){
		startSound.Play ();
		GameState.initializeSettings (GameMode.STOCK, GameState.playerCount, GameState.playerLives, GameState.winScore, 300.0f, level);
		Invoke ("LoadLevelAfterSoundEnd", 2.0f);
	}

	public void LoadLevelAfterSoundEnd () {
		SceneManager.LoadScene ("xbox");
	}

	public void SetNumOfPlayers(){
		settingsButton.SetActive (false);
		platforms.SetActive (false);

		swordSound2.Play();
		GameState.playerCount = numOfPlayers;

		title.text = "Fight Settings: ";

		// list out all the UI elements
		settingGame.SetActive(true);

		// default to timer option
		GameState.gameMode = GameMode.TIMER;
		stockOption.SetActive (false);
		GameState.winScore = 1;
		GameState.playerLives = -1;
		GameState.gameTime = 1.0f;

		updateModeText ();
		updateTimeText ();
		updateLifeText ();
		updateRoundText ();

		loadLevelButton.SetActive (true);
	}

	public void SetMap(){
		
		settingGame.SetActive(false);
		loadLevelButton.SetActive (false);

		title.text = "Choose the room!";

		map1Button.SetActive (true);
		map2Button.SetActive (true);
	}

	public void alternateGameMode(){
		if (GameState.gameMode == GameMode.STOCK) 
		{
			GameState.gameMode = GameMode.TIMER;

			// hide stock option and clear stock
			stockOption.SetActive(false);
			GameState.playerLives = -1;

			// show timer option
			timerOption.SetActive(true);

			updateModeText();
			updateTimeText();
		} 
		else if (GameState.gameMode == GameMode.TIMER)
		{
			GameState.gameMode = GameMode.STOCK;
			GameState.playerLives = 1;

			// show stock option
			stockOption.SetActive(true);

			// hide timer option and clear time
			timerOption.SetActive(false);

			updateModeText();
			updateTimeText();
		}
	}

	public void increaseStock(){
		++GameState.playerLives;
		updateLifeText();
	}

	public void decreaseStock(){
		if (GameState.playerLives > 1) {
			--GameState.playerLives;		
			updateLifeText();
		}
	}

	public void increaseRounds(){
		++GameState.winScore;
		updateRoundText();
	}

	public void decreaseRounds(){
		if (GameState.winScore > 0) {
			--GameState.winScore;
			updateRoundText();
		}
	}

	public void increaseTime(){
		GameState.gameTime += 1.0f;
		updateTimeText ();
	}

	public void decreaseTime(){
		if (GameState.gameTime > 0) {
			GameState.gameTime -= 1.0f;
			updateTimeText ();
		}
	}

	void updateTimeText(){
		timeNumber.text = "" + GameState.gameTime + " MIN";
	}

	void updateLifeText(){
		stockNumber.text = "" + GameState.playerLives;
	}

	void updateRoundText(){
		roundNumber.text = "" + GameState.winScore;
	}

	void updateModeText(){
		gameMode.text = GameState.gameMode.ToString();
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
