using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text winnerText;
    AudioSource swordSound;
    string levelName;

	// Use this for initialization
	void Start () {
        winnerText.text = "Sleepy Player " + GameState.winner + " Wins!";
        swordSound = GetComponent<AudioSource>();
    }

    public void loadScene(string name)
    {
        swordSound.Play();
        Invoke("LoadLevelAfterSoundEnd", 2.0f);
        levelName = name;
    }

    public void LoadLevelAfterSoundEnd()
    {
        GameState.loadScene(levelName);
    }
}
