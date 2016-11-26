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

	public override void PressAttack(ControlButton button) {
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

		latchWep.Launch ();
	}


	public override void ReleaseAttack (ControlButton button) 
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}


		latchWep.Pull ();
	}
}
