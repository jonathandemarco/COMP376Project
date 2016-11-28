using UnityEngine;
using System.Collections;

public class MovingMenuCamera : MonoBehaviour {
		

	public float factor;
	private bool firstCycle;
	private float time;
	private float timeToRotate;
	private float increment;

	public Material sky;
	public float speed;

	void Start(){
		firstCycle = true;
		RenderSettings.skybox = sky;
		time = 0.0f;
		timeToRotate = 0.0f;
	}
	void Update()  {
		timeToRotate += Time.deltaTime;

		if(time > 0.0f && time < factor){
			RenderSettings.skybox.SetFloat ("_Blend", time/factor);
		}
		else if (time > factor) {
			time = factor;
			firstCycle = false;
		} else if (time < 0.0f) {
			time = 0.0f;
			firstCycle = true;
		} 

		if (firstCycle) {
			time += Time.deltaTime;
		} else if (!firstCycle) {
			time -= Time.deltaTime;
		}

		transform.rotation = Quaternion.Euler (-40, -65 + speed * timeToRotate, 0);
	}
}

