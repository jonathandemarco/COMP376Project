using UnityEngine;
using System.Collections;

public class Latch : Weapon {

	public GameObject handle;
	public float maxDistance;

	private bool isUsed;
	private bool isLaunched;
	private bool collided;
	private Renderer [] renderers;
	private float time;
	private Vector3 initialLocal;

	// Use this for initialization
	void Start () {
		isUsed = false;
		isLaunched = false;
		collided = false;
		time = 0.0f;

		initialLocal = transform.localPosition;
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
				if (time < 0.5f) {
					for (int r = 0; r < renderers.Length; ++r) {
						renderers [r].enabled = false;
					}

					isUsed = false;
					collided = false;
					time = 0.0f;
				} else {
					transform.position += handle.transform.right / 2;
				} 
			}
		}
	}

	public void Launch(Renderer [] rend){
		transform.localPosition = initialLocal;
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
