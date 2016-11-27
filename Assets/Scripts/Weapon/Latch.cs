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
	private float time;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		collided = false;
		movingDistance = new Vector3(0, 0, maxDistance);
		time = 0.0f;
	}
	
	// Update is called once per frame
	public override void Update () {
	
		if (isUsed) {
			if (isLaunched) {
				time += Time.deltaTime;
				if (time < 2.0f) {
					transform.localPosition += movingDistance / 10;
				} else {
					Pull ();
				}
			} else {	
				time -= Time.deltaTime;
				if (time < 0.0f) {
					for (int r = 0; r < renderers.Length; ++r) {
						renderers [r].enabled = false;
					}

					isUsed = false;
					collided = false;
					time = 0.0f;
				} else {
					transform.localPosition += -movingDistance / 10;
				} 
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
