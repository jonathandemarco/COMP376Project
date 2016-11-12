using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

	Renderer[] renderers;
	public GameObject laserMachinePrefab;
	public GameObject laserRenderer;
	public GameObject laserApparatusPrefab;

	private GameObject laserApparatus;
	private GameObject laserLine;
	private LaserBoundary laser;

	private int count;

	void Start() {

		count = 0;
		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
	}

	public override void PressAttack(ControlButton button) {
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

		GameObject machine = Instantiate (laserMachinePrefab, transform.position, laserMachinePrefab.transform.rotation) as GameObject;
		machine.GetComponent<Rigidbody> ().AddForce (machine.transform.forward);
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
		}
	}
		
	public override void ReleaseAttack (ControlButton button) 
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
	}
}
