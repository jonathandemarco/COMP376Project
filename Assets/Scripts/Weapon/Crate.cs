using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	public int IDValue; //To give the specefic item from database to player

	public float delta = 1.5f;  // Amount to move left and right from the start point
	public float speed = 2.0f; 
	public float idleTime = 10.0f;
	public int max_size; // weapon database size
	private float inactiveTime;
	private bool grounded;

	void Start(){
		grounded = false;
		IDValue = Random.Range(1, max_size); 
	}
		
	void Update () {
		if (grounded) {
			if (inactiveTime < idleTime)
				inactiveTime += Time.deltaTime;
			else
				Destroy (gameObject);
		}
		else
			transform.rotation = Quaternion.Euler (0, 0, delta * Mathf.Sin(speed * Time.time));
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.collider.gameObject.layer == LayerMask.NameToLayer ("Terrain"))
			grounded = true;
	}
}
