using UnityEngine;
using System.Collections;

public class Pillow : Weapon {

	Renderer[] renderers;

	public void Start() {
		GetComponent<Collider>().enabled = false;
		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
	}

	public override void PressAttack(ControlButton button)
	{
		Debug.Log("Swing Pillow");

		// allowAttack() will tell us if the user is allowed to perform an attack
		// function should check if the same button is pressed
		// if(button.allowAttack()){
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}			

		EnableCollider();
		startAnimation ();
		// }
	}

	// called by animation of sword
	public void StopAttack()
	{
		DisableCollider();
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
		stopAnimation ();
	}

}
