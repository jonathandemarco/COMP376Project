using UnityEngine;
using System.Collections;

public class Hook : Weapon {
	
	public GameObject latch;
	Renderer[] renderers;

	private bool inUse;

	private Latch latchWep;


	void Start() {
		inUse = false;
		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		latchWep = latch.GetComponent<Latch> ();
		latchWep.setPlayerChar (getPlayerChar ());
	}

	public override void PressAttack(InputSystem button) {
		if (!inUse) {
			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].enabled = true;
			}

			latchWep.Launch (renderers);

			inUse = true;
		}
	}

	public void resetState(){
		inUse = false;
	}
}
