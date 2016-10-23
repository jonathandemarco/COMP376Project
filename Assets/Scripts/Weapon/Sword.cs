using UnityEngine;
using System.Collections;

public class Sword : Weapon {
	void Start () {
		SetType (WeaponType.Melee);
		SetDamage(4);
		SetAttackRate (1.5f);
		SetAnimator();
	}

	public override void PressAttack(){
		// call the play animation;

	}
}
