using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	GravitySource gSource;

	private bool isUsed;
	private bool isLaunched;
	private bool collided;
	private Renderer [] renderers;
	private float time;
	private Vector3 initialLocal;
	private GameObject latchedObject;
	private Transform parentOfLatchedObject;
	private bool latchedOn;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		collided = false;
		latchedOn = false;
		time = 0.0f;

		initialLocal = transform.localPosition;
		GetComponent<Collider> ().enabled = false;
	}
	
	// Update is called once per frame
	public override void Update () {
	
		if (isUsed) {
			if (isLaunched) {
				time += Time.deltaTime;
				if (time < 1.0f) {
					transform.position += -handle.transform.right / 5;
				} else {
					Pull ();
				}
			} else {	
				time -= Time.deltaTime;
				if (time < 0.20f) {
					for (int r = 0; r < renderers.Length; ++r) {
						renderers [r].enabled = false;
					}

					GetComponent<Collider> ().enabled = false;

					isUsed = false;
					collided = false;
					time = 0.0f;


					if (latchedOn) {
						// reset the latched object to its initial parent
						latchedObject.transform.parent = parentOfLatchedObject;

						// reset the gameObject and the transforms
						parentOfLatchedObject = null;
						latchedObject = null;

						latchedOn = false;
					}


				} else {
					transform.position += handle.transform.right / 4;
				} 
			}
		}
	}

	public void Launch(Renderer [] rend){		
		transform.localPosition = initialLocal;
		renderers = rend;
		isUsed = true;
		isLaunched = true;
		GetComponent<Collider> ().enabled = true;
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
			Debug.Log ("Latched on");
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager> ();
			char colPlayerChar = getPlayerChar ();

			if (manager.getPlayerChar () != colPlayerChar) {
				if (!latchedOn) {
					latchedOn = true;
					latchedObject = c.gameObject;
					parentOfLatchedObject = c.gameObject.transform.parent;
					c.gameObject.transform.parent = transform;

					// Pull the player back
					collided = true;
					if (isLaunched) {
						Pull ();
					}

				}

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
			if (time < 0.5f) {
				time += Time.deltaTime * 5;
			}
			Pull ();
		}
	}
}
