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

		StartCoroutine (Swing ());
	}

	private IEnumerator Swing(){
		//TODO animation of weapon

		//TODO Find the exact time for the removal of the sword
		yield return new WaitForSeconds (0.5f);

		this.gameObject.SetActive (false);
	}
}
