using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	private bool isUsed;
	private bool isLaunched;
	private bool collided;
	private float time;
	private GameObject latchedObject;
	private Transform parentOfLatchedObject;
	private bool latchedOn;

	// Use this for initialization
	public override void Start () {
		isUsed = false;
		isLaunched = false;
		collided = false;
		latchedOn = false;
		time = 0.0f;
		GetComponent<Collider> ().enabled = false;
	}

	// Update is called once per frame
	public override void Update () {
		if (isUsed) {
			if (isLaunched) {
				time += Time.deltaTime;
				if (time < 0.5f) {
					transform.position -= handle.transform.right / 2;
				} else {
					Pull ();
				}
			} else {
				time -= Time.deltaTime;
				if ((handle.transform.position - transform.position).magnitude < 3.5f || time < 0.1f) {
					GetComponent<Collider> ().enabled = false;

					isUsed = false;
					collided = false;
					time = 0.0f;

					transform.position = handle.transform.position;

					if (latchedOn) {
						// reset the latched object to its initial parent
						latchedObject.transform.parent = parentOfLatchedObject;

						// reset the gameObject and the transforms
						parentOfLatchedObject = null;
						latchedObject = null;

						latchedOn = false;
					}

					transform.parent.GetComponent<Hook> ().resetState ();
					transform.parent.GetComponent<Hook> ().hide ();

				} else {
					transform.position += handle.transform.right / 2;
				} 
			}
		} else {
			transform.position = handle.transform.position;
			transform.localPosition = handle.transform.localPosition;
			transform.localRotation = handle.transform.localRotation;
		}
	}

	public void Launch(){
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


		if (col.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			Debug.Log ("Latched on");
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager> ();
			char colPlayerChar = getPlayerChar ();

			if (manager.getPlayerChar () != colPlayerChar) 
			{
				MessagePassingHelper.passMessageOnCollision (this, col);
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
			MessagePassingHelper.passMessageOnCollision (this, col);
			collided = true;
			Pull ();
		}
	}
}
