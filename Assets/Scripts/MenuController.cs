using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	void Update(){
		if (Input.GetKeyDown ("space")) {
			LoadLevel ();
		}
	}

	public void LoadLevel(){
		SceneManager.LoadScene ("Gladiators");
	}

	public void printStuff(){
		Debug.Log ("Hello");
	}


}
