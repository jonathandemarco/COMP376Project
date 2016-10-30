using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sword : Weapon
{
    Renderer[] renderers;

    public override void PressAttack(ControlButton button)
    {
        Debug.Log("Swing");

		// allowAttack() will tell us if the user is allowed to perform an attack
		// function should check if the same button is pressed
		// if(button.allowAttack()){
			renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
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
