using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sword : Weapon
{
    Renderer[] renderers;
	public AudioClip swordSound2;

    public void Start() {
        GetComponent<Collider>().enabled = false;
        renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
    }

	public override void PressAttack(InputSystem button)
    {
		AudioSource audioSource = GetComponent<AudioSource> ();

		int whichsound = Random.Range (0, 2);
		if (whichsound == 0)
			audioSource.clip = swordSound2;
		else
			audioSource.clip = attackSound;
		audioSource.Play ();
			
        Debug.Log("Swing");

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
		loseDurability(1);

    }
}
