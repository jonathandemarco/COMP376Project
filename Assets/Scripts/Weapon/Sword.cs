using UnityEngine;
using System.Collections;

public class Sword : Weapon {
	
	void Start () {
		SetType (WeaponType.Melee);
		SetDamage(4);
		SetAttackRate (1.5f);
		SetAnimator ();
		SetAudioSource ();
		SetColliderInactive ();
	}

	//to-test
	void Update(){
		PressAttack ();
	}

	public override void PressAttack(){
		// set conditions
		// call the play animation;
		YieldAttackAnimator();
	}
}
