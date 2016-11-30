using UnityEngine;
using System.Collections;

public class Pillow : Weapon {

	public override void Start() {
		base.Start ();
		GetComponent<Collider>().enabled = false;
//		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
	}

	public override void PressAttack(InputSystem button)
	{
		Debug.Log("Swing Pillow");

		// allowAttack() will tell us if the user is allowed to perform an attack
		// function should check if the same button is pressed
		// if(button.allowAttack()){
		display();		
		EnableCollider();
		startAnimation ();
		// }

	}

	// called by animation of sword
	public void StopAttack()
	{
		DisableCollider();
		hide ();
		stopAnimation ();
		loseDurability(0);
	}

}
