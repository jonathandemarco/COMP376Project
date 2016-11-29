using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

	public GameObject laserMachinePrefab;
	public GameObject laserRenderer;
	public GameObject laserApparatusPrefab;
	public Transform laserSpawn;
	public float force;

	private float time;
	private bool canShoot;
	private GameObject laserApparatus;
	private GameObject laserLine;
	private LaserBoundary laser;

	private int count;

	public override void Start() {
		base.Start ();
		count = 0;
		time = 0.0f;
		canShoot = true;
	}

	public override void Update ()
	{
		base.Update ();

		if (time >= attackRate)
		{
			canShoot = true;
			time = 0.0f;
		} 
		else if (time < attackRate && !canShoot) 
		{
			time += Time.deltaTime;
		}
	}

	public override void PressAttack(InputSystem button) {
		if (canShoot) {
			display ();
			GameObject machine = Instantiate (laserMachinePrefab, laserSpawn.position, laserSpawn.rotation) as GameObject;
			machine.GetComponent<Rigidbody> ().AddForce (-transform.right * force);
			machine.GetComponent<LaserMachine> ().setPlayerChar (getPlayerChar ());
			machine.GetComponent<LaserMachine> ().setPlayerOwner (this.getPlayerOwner ());
			count++;

			if (count == 1) {
				// make gameObject to contain both laser machines
				laserApparatus = Instantiate (laserApparatusPrefab, laserApparatusPrefab.transform.position, laserApparatusPrefab.transform.rotation) as GameObject;

				// the machine will be paired up
				machine.GetComponent<LaserMachine> ().IsPairedUp ();
				machine.GetComponent<LaserMachine> ().setPlayerChar (getPlayerChar ());
				machine.GetComponent<LaserMachine> ().setPlayerOwner (this.getPlayerOwner ());

				// set the machine as a child of the laser apparatus
				machine.transform.parent = laserApparatus.transform;

				// create the line renderer
				laserLine = Instantiate (laserRenderer, laserRenderer.transform.position + transform.position, laserRenderer.transform.rotation, laserApparatus.transform) as GameObject;
				laserLine.GetComponent<LaserBoundary> ().setPlayerChar (getPlayerChar ());
				laserLine.GetComponent<LaserBoundary> ().setPlayerOwner (this.getPlayerOwner ());

				// get the script in order to assign origin
				laser = laserLine.GetComponent<LaserBoundary> ();
				laser.origin = machine.transform;
			} else if (count == 2) {
				// enable the life time count down
				laserApparatus.GetComponent<LaserApparatus> ().Charge ();

				machine.transform.parent = laserApparatus.transform;
				laser.destination = machine.transform;
				laser.RenderLine ();

				count = 0;
				loseDurability (1);

				// create timer for shooting
				canShoot = false;
			}

			AudioSource audioSource = GetComponent<AudioSource> ();
			audioSource.clip = attackSound;
			audioSource.Play ();
		}
	}
		
	public override void ReleaseAttack (InputSystem button) 
	{
		hide ();
	}

	public void decreaseNumOfMachines(){
		if (count > 0) {
			--count;
		}
	}
}
