using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	private bool isUsed;
	private bool isLaunched;
	private bool collided;
	private Vector3 initialDistance;
	Vector3 movingDistance;
	private Renderer [] renderers;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		collided = false;
		initialDistance = handle.transform.position - transform.position;
		movingDistance = new Vector3(0, 0, maxDistance);
	}
	
	// Update is called once per frame
	public override void Update () {
	
		if (isUsed) {
			Vector3 currentDistance = handle.transform.position - transform.position;
			float distanceBetweenWep = (currentDistance - initialDistance).magnitude;
		

			if (isLaunched) {
				if (distanceBetweenWep < maxDistance) {
					transform.position += movingDistance * 5.0f;
				} else if (distanceBetweenWep >= maxDistance) {					
					Pull ();
				}
			}
		}
		else{
			movingDistance = movingDistance / 1.5f;
			if (movingDistance.magnitude < 1.0f) {
				for (int r = 0; r < renderers.Length; ++r) {
					renderers [r].enabled = false;
				}

				movingDistance = new Vector3(0, 0, maxDistance);

				isUsed = false;
				collided = false;
			} 
			if(isUsed) {
				transform.position += -movingDistance * 5.0f;
			} 
		}
	}

	public void Launch(Renderer [] rend){
		renderers = rend;
		isUsed = true;
		isLaunched = true;
	}

	public void Pull(){
		isLaunched = false;
	}

	public bool GetUse(){
		return isUsed;
	}

	public override void OnCollisionEnter(Collision c)
	{
		Collider col = c.collider;

		MessagePassingHelper.passMessageOnCollision (this, col);

		if (col.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			Debug.Log ("Boom");
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager> ();
			char colPlayerChar = getPlayerChar ();

			if (manager.getPlayerChar () != colPlayerChar) {
				if (impactSound != null) {
					AudioSource audioSource = GetComponent<AudioSource> ();
					if (audioSource != null) {
						audioSource.clip = impactSound;
						audioSource.Play ();
					}
				}
			}
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("Terrain") && !collided){
			collided = true;
			Pull ();
		}
	}

}
