using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sword : Weapon
{
	public AudioClip swordSound2;

    public override void Start() {
		base.Start ();
        GetComponent<Collider>().enabled = false;
//        renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
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
		loseDurability(1);

    }
}
