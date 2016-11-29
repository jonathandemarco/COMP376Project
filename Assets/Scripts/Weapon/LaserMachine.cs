using UnityEngine;
using System.Collections;

public class LaserMachine : Weapon {

	private float lifeTime;
	private bool isAlone;

	void Start(){
		isAlone = true;
		lifeTime = 0.0f;
	}

	public override void Update ()
	{
		if (isAlone) {
			lifeTime += Time.deltaTime;

			if (lifeTime > 3.0f) {
				// decrease the count of the machines by getting the LaserGun component
				transform.parent.GetComponent<LaserGun> ().decreaseNumOfMachines ();

				// TODO: initiate explosion

				// destroy the object
				Destroy (gameObject);
			}
		} else {
			lifeTime = 0.0f;
		}
	}


	public override void OnCollisionEnter(Collision c)
	{
		// Set the laser machiine to the collision point
		Collider col = c.collider; 
		Transform collidedObject = col.gameObject.transform;
		Vector3 collisionPoint = collidedObject.position - c.contacts [0].point;

		// Disable the collider so it is not affected by the laser line

		gameObject.GetComponent<Collider>().enabled = false;

		MessagePassingHelper.passMessageOnCollision (this, col);

		if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
		{

			Debug.Log("Boom");
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager>();
			char colPlayerChar = getPlayerChar();

			if (manager.getPlayerChar () == getPlayerChar ()) {
				return;
			}

			if (impactSound != null) {
				AudioSource audioSource = GetComponent<AudioSource>();
				if (audioSource != null) {
					audioSource.clip = impactSound;
					audioSource.Play ();
				}
			}
		}
	}

	public void IsPairedUp(){
		isAlone = false;
	}
}
