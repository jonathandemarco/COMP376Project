using UnityEngine;
using System.Collections;

public class Vacuum : Weapon {
	Renderer[] renderers;


	public void Start() {

		renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

	}
	public override void PressAttack(ControlButton button) {
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

	}
	public override void ReleaseAttack (ControlButton button) 
	{
		Debug.Log ("Released");

		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
	}
}
