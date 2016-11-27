using UnityEngine;
using System.Collections;
// Holds the two machines 
public class LaserApparatus : MonoBehaviour {

	public float lifeTime;
	private float actualTime;
	private bool isCharged;

	// Use this for initialization
	void Start () {
		actualTime = 0.0f;
		isCharged = false;
	}
	
	// Create lifeTime for laser machines
	void Update () {
		if (isCharged) {
			if (actualTime > lifeTime) {
				Destroy (gameObject);
			}

			actualTime += Time.deltaTime;
		}
	}

	public void Charge(){
		isCharged = true;
	}
}
