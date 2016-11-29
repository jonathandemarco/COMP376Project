using UnityEngine;
using System.Collections;

public class Hook : Weapon {
	
	public GameObject latch;
	private bool inUse;
	private Latch latchWep;

	void Start() {
		base.Start ();
		inUse = false;
		latchWep = latch.GetComponent<Latch> ();
		latchWep.setPlayerChar (getPlayerChar ());
		latchWep.setPlayerOwner (getPlayerOwner ());
	}

	public override void PressAttack(InputSystem button) {
		if (!inUse) {
			display ();
			latchWep.Launch ();
			inUse = true;
			loseDurability (1);
		}
	}

	public void resetState(){
		inUse = false;
	}
}
