using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	void Start () {
		SetType (WeaponType.Melee);
		SetDamage(10);
		SetAttackRate (1.5f);
		SetAnimator ();
		SetAudioSource ();
	}

	//to-test

	public override void PressAttack(ControlButton button)
    {
		this.gameObject.SetActive (true);
		EnableCollider ();
		StartCoroutine (Swing ());
	}

	private IEnumerator Swing(){
		//TODO animation of weapon

		//TODO Find the exact time for the removal of the sword
		yield return new WaitForSeconds (0.75f);
		DisableCollider ();
		this.gameObject.SetActive (false);
	}
}
