using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

	public GameObject laserMachinePrefab;
	public GameObject laserRenderer;
	public GameObject laserApparatusPrefab;
	public Transform laserSpawn;
	public float force;

	private GameObject laserApparatus;
	private GameObject laserLine;
	private LaserBoundary laser;

	private int count;

	public override void Start() {
		base.Start ();
		count = 0;
//		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
	}

	public override void PressAttack(InputSystem button) {
		display ();

		GameObject machine = Instantiate (laserMachinePrefab, laserSpawn.position, laserSpawn.rotation) as GameObject;
		machine.GetComponent<Rigidbody> ().AddForce (-transform.right * force);
		count++;

		if (count == 1) {
			// make gameObject to contain both laser machines
			laserApparatus = Instantiate (laserApparatusPrefab, laserApparatusPrefab.transform.position, laserApparatusPrefab.transform.rotation) as GameObject;
			machine.transform.parent = laserApparatus.transform;

			// create the line renderer
			laserLine = Instantiate (laserRenderer, laserRenderer.transform.position + transform.position, laserRenderer.transform.rotation, laserApparatus.transform) as GameObject;

			// get the script in order to assign origin
			laser = laserLine.GetComponent<LaserBoundary> ();
			laser.origin = machine.transform;
		}

		else if (count == 2) {
			// enable the life time count down
			laserApparatus.GetComponent<LaserApparatus>().Charge();

			machine.transform.parent = laserApparatus.transform;
			laser.destination = machine.transform;
			laser.RenderLine ();

			count = 0;
			loseDurability(1);
		}

		AudioSource audioSource = GetComponent<AudioSource> ();
		audioSource.clip = attackSound;
		audioSource.Play ();
	}
		
	public override void ReleaseAttack (InputSystem button) 
	{
		hide ();
	}
}
