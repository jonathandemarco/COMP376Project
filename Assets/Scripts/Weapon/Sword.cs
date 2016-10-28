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


	public override void PressAttack(ControlButton button)
    {
		// set conditions
		// call the play animation;
		YieldAttackAnimator();
	}
}
