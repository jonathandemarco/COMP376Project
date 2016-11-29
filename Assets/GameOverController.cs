using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text winnerText;

	// Use this for initialization
	void Start () {
        winnerText.text = "Sleepy Player " + GameState.winner + " Wins!";
    }

    public void loadScene(string name)
    {
        GameState.loadScene(name);
    }
}
