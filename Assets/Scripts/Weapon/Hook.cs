using UnityEngine;
using System.Collections;

public class Hook : Weapon {
	
	public GameObject latch;
	Renderer[] renderers;
	private Latch latchWep;

	void Start() {
		renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		latchWep = latch.GetComponent<Latch> ();
	}

	public override void PressAttack(InputSystem button) {
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

		latchWep.Launch (renderers);
	}
}
