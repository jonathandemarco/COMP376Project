using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XboxSceneController : MonoBehaviour {

    private float tTime = 0.0f;
    public Text loadingText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        tTime += Time.deltaTime;

        if (tTime > 7.0f)
        {
            SceneManager.LoadScene(GameState.level);
        }
	}
}
