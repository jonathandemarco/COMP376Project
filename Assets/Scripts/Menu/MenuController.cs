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
	//Icons representing player that will play
	public GameObject player1_image;
	public GameObject player2_image;
	public GameObject player3_image;
	public GameObject player4_image;
	//Buttons appearing after Start has been pressed (# of players)
	public GameObject p2Button;
	public GameObject p3Button;
	public GameObject p4Button;
	//choosing map buttons
	public GameObject map1Button;
	public GameObject map2Button;
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

		if(Input.GetKeyDown("space") && !hasPressedStart){
			hasPressedStart = true;
			pressStartText.enabled = false;
			CancelInvoke ();
			p2Button.SetActive(true);
			p3Button.SetActive(true);
			p4Button.SetActive(true);
			swordStart.Play();
		}

		AddPlayers ();

	}

	public void LoadLevel(string level){
		startSound.Play ();
		GameState.initializeSettings (GameMode.STOCK, GameState.playerCount, 1, 300.0f, level);
		Invoke ("LoadLevelAfterSoundEnd", 3.0f);
	}

	public void LoadLevelAfterSoundEnd () {
		SceneManager.LoadScene ("xbox");
	}

	public void SetNumOfPlayers(int num){
		swordSound2.Play();
		GameState.playerCount = num;
		p2Button.SetActive (false);
		p3Button.SetActive (false);
		p4Button.SetActive (false);
		map1Button.SetActive (true);
		map2Button.SetActive (true);
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
