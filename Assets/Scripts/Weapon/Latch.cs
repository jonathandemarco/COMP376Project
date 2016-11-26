using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	private bool isUsed;
	private bool isLaunched;
	private bool alreadyCollided;
	private float time;
	private Vector3 initialDistance;
	private Renderer [] renderers;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		alreadyCollided = false;
		time = 0.0f;
		initialDistance = handle.transform.localPosition - transform.localPosition;
	}
	
	// Update is called once per frame
	public override void Update () {
	
		if (isUsed) {
			Vector3 currentDistance = handle.transform.localPosition - transform.localPosition;
			float distanceBetweenWep = (currentDistance - initialDistance).magnitude;

			if (isLaunched && distanceBetweenWep < maxDistance) {
				transform.position += -handle.transform.right * time;
				time += Time.deltaTime;
			} else if (isLaunched && distanceBetweenWep >= maxDistance) {
				// reached max distance
				time = 0.0f;
				time += Time.deltaTime;
				Pull ();
			} else if (!isLaunched && isUsed && distanceBetweenWep > 1.05f) {
				transform.position += handle.transform.right * time;
				time += Time.deltaTime;
			} else if (!isLaunched && isUsed && distanceBetweenWep <= 1.05f){
				for(int r = 0; r < renderers.Length; ++r){
					renderers [r].enabled = false;
				}
				time = 0.0f;
				isUsed = false;
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
				if (weaponSound != null) {
					AudioSource audioSource = GetComponent<AudioSource> ();
					if (audioSource != null) {
						audioSource.clip = weaponSound;
						audioSource.Play ();
					}
				}
			}
		} else if(!alreadyCollided && isLaunched){
			time = 0.0f;
			alreadyCollided = true;
			Pull ();
		}
	}
}
