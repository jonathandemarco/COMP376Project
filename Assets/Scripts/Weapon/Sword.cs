﻿using UnityEngine;
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

        	Debug.Log(this.gameObject);
        	StartCoroutine(Swing());
		// }
    }

    private IEnumerator Swing()
    {
        //TODO animation of weapon

        //TODO Find the exact time for the removal of the sword
        yield return new WaitForSeconds(0.75f);
        DisableCollider();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
    }
}
